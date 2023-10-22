using CMS_appBackend.DTOs;
using CMS_appBackend.DTOs.RequestModels;
using CMS_appBackend.DTOs.ResponseModels;
using CMS_appBackend.Entities;
using CMS_appBackend.Interface.Repositories;
using CMS_appBackend.Interface.Services;

namespace CMS_appBackend.Implementations.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IBlogRepository _blogRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PostService(
            IPostRepository postRepository,
            IBlogRepository blogRepository,
            IWebHostEnvironment webHostEnvironment
        )
        {
            _postRepository = postRepository;
            _blogRepository = blogRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<BaseResponse> CreatePostAsync(CreatePostRequestModel model)
        {
            var post = await _postRepository.GetAsync(
                x => x.Content == model.Content && x.IsDeleted == false
            );
            if (post != null)
            {
                return new BaseResponse() { Message = "Post Already Exist", Success = false, };
            }
            var imageName = "";
            if (model.PostImage != null)
            {
                var imgPath = _webHostEnvironment.WebRootPath;
                var imagePath = Path.Combine(imgPath, "images");
                Directory.CreateDirectory(imagePath);
                var imageType = model.PostImage.ContentType.Split('/')[1];
                imageName = $"{Guid.NewGuid()}.{imageType}";
                var fullPath = Path.Combine(imagePath, imageName);
                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    model.PostImage.CopyTo(fileStream);
                }
            }
            var pos = new Post
            {
                Content = model.Content,
                PostImage = imageName,
                CreatedOn = DateTime.Now,
                PostName = model.PostName,
                PostTag = model.PostTag,
            };
            await _postRepository.CreateAsync(pos);
            return new BaseResponse { Message = "Post successfully created", Success = true, };
        }

        public async Task<BaseResponse> GetPostByIdAsync(int id)
        {
            var post = await _postRepository.GetPostById(id);
            if (post == null)
            {
                return new BaseResponse { Message = "Post not found", Success = false, };
            }
            return new BaseResponse { Message = "Post found", Success = true, };
        }

        public async Task<BaseResponse> UpdatePost(UpdatePostRequestModel model)
        {
            var post = await _postRepository.GetAsync(
                x => x.PostTag == model.PostTag && x.IsDeleted == false
            );
            if (post == null)
            {
                return new BaseResponse { Message = "Post not found", Success = false, };
            }
            IFormFile imageFile;
            SaveImage(model.PostImage, out imageFile);
            if (imageFile != null)
            {
                model.PostImage = imageFile;
            }
            post.Content = model.Content;
            post.PostName = model.PostName;
            post.PostTag = model.PostTag;
            post.PostImage = model.PostImage.FileName;
            post.CreatedOn = DateTime.Now;
            await _postRepository.UpdateAsync(post);
            return new BaseResponse { Message = "Post Updated Successfully", Success = true, };
        }

        public async Task<BaseResponse> DeletePost(int id)
        {
            var post = await _postRepository.GetAsync(x => x.Id == id && x.IsDeleted == false);
            if (post == null)
            {
                return new BaseResponse { Message = "Post not found", Success = false, };
            }
            post.IsDeleted = true;
            await _postRepository.UpdateAsync(post);
            return new BaseResponse { Message = "Post Deleted Successfully", Success = true, };
        }

        public async Task<PostsResponseModel> GetAllPost()
        {
            var post = await _postRepository.GetAllAsync();
            return new PostsResponseModel
            {
                Data = post.Select(
                        x =>
                            new PostDto
                            {
                                Content = x.Content,
                                PostName = x.PostName,
                                PostTag = x.PostTag,
                                PostImage = x.PostImage,
                                CreatedOn = x.CreatedOn,
                            }
                    )
                    .ToList(),
                Message = "Posts found successfully",
                Success = true,
            };
        }

        private void SaveImage(IFormFile imageFile, out IFormFile savedImageFile)
        {
            savedImageFile = null;
            if (imageFile == null || imageFile.Length <= 0)
            {
                return;
            }
            var imgPath = _webHostEnvironment.WebRootPath;
            var imagePath = Path.Combine(imgPath, "Images");
            Directory.CreateDirectory(imagePath);
            var imageType = imageFile.ContentType.Split('/')[1];
            var imageName = $"{Guid.NewGuid()}.{imageType}";
            var fullPath = Path.Combine(imagePath, imageName);
            using (var filestream = new FileStream(fullPath, FileMode.Create))
            {
                imageFile.CopyTo(filestream);
            }
            var savedImagePath = $"/Images/{imageName}";
            var memoryStream = new MemoryStream(File.ReadAllBytes(fullPath));
            savedImageFile = new FormFile(
                memoryStream,
                0,
                memoryStream.Length,
                null,
                Path.GetFileName(memoryStream.ToString())
            )
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/" + imageType
            };
        }
    }
}

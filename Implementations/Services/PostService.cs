using CMS_appBackend.DTOs;
using CMS_appBackend.DTOs.RequestModels;
using CMS_appBackend.DTOs.ResponseModels;
using CMS_appBackend.Entities;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using CMS_appBackend.Interface.Repositories;
using CMS_appBackend.Interface.Services;

namespace CMS_appBackend.Implementations.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IBlogRepository _blogRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IImageRepository _imageRepository;
        private readonly CloudinaryService _cloudinaryService;

        public PostService(
            IPostRepository postRepository,
            IBlogRepository blogRepository,
            IWebHostEnvironment webHostEnvironment,
            CloudinaryService cloudinaryService,
            IImageRepository imageRepository
        )
        {
            _postRepository = postRepository;
            _blogRepository = blogRepository;
            _webHostEnvironment = webHostEnvironment;
            _cloudinaryService = cloudinaryService;
            _imageRepository = imageRepository;
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
            string cloudinaryUrl = null;

            if (model.PostImage != null)
            {
                cloudinaryUrl = await _cloudinaryService.UploadImageToCloudinaryAsync(model.PostImage);
            }
            var pos = new Post
            {
                Content = model.Content,
                PostImage = cloudinaryUrl,
                CreatedOn = DateTime.Now,
                PostName = model.PostName,
                PostTag = model.PostTag,
            };
            await _postRepository.CreateAsync(pos);
            return new BaseResponse { Message = "Post successfully created", Success = true, };
        }

        public async Task<PostResponseModel> GetPostByIdAsync(int id)
        {
            var post = await _postRepository.GetPostById(id);
            if (post == null)
            {
                return new PostResponseModel { Message = "Post not found", Success = false, };
            }
            return new PostResponseModel
            {
                Data = new PostDto
                {
                    Content = post.Content,
                    PostName = post.PostName,
                    PostTag = post.PostTag,
                    PostImage = post.PostImage,
                },
                Message = "Post found successfully",
                Success = true,
            };
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
            if (model.PostImage != null)
            {
                string cloudinaryImageUrl = await _cloudinaryService.UploadImageToCloudinaryAsync(model.PostImage);
                post.PostImage = cloudinaryImageUrl;
            }
            post.Content = model.Content ?? post.Content;
            post.PostName = model.PostName ?? post.PostName;
            post.PostTag = model.PostTag ?? post.PostTag;
            post.CreatedOn = DateTime.UtcNow;
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
                            }
                    )
                    .ToList(),
                Message = "Posts found successfully",
                Success = true,
            };
        }
        // private string GetImageUrlFromPublicId(string publicId)
        // {
        //     return _cloudinaryService.GetImageUrl(publicId);
        // }

    }
}

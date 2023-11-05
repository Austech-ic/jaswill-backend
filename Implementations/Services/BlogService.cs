using CMS_appBackend.Identity;
using CMS_appBackend.DTOs.ResponseModels;
using CMS_appBackend.Implementations.Repositories;
using CMS_appBackend.Interface.Services;
using Microsoft.EntityFrameworkCore;
using CMS_appBackend.DTOs;
using CMS_appBackend.Interface.Repositories;
using CMS_appBackend.Entities;
using CMS_appBackend.DTOs.RequestModels;


namespace CMS_appBackend.Implementations.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IPostRepository _postRepository;
         private readonly  IWebHostEnvironment _webHostEnvironment;

        public BlogService(IBlogRepository repository, IPostRepository postRepository,IWebHostEnvironment webHostEnvironment)
        {
            _blogRepository = repository;
            _postRepository = postRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<BaseResponse> CreateBlogAsync(CreateBlogRequestModel model)
        {
            var blog = await _blogRepository.GetAsync(x => x.ContentName == model.ContentName && x.IsDeleted == false);
            if (blog != null)
            {
                return new BaseResponse()
                {
                    Message = "Blog Already Exist",
                    Success = false,
                };
            }
            var imageName = "";
            if(model.ImageUrl != null)
            {
                var imgPath = _webHostEnvironment.WebRootPath;
                var imagePath  = Path.Combine(imgPath,"images");
                Directory.CreateDirectory(imagePath);
                var imageType = model.ImageUrl.ContentType.Split('/')[1];
                imageName = $"{Guid.NewGuid()}.{imageType}";
                var fullPath = Path.Combine(imagePath,imageName);
                using(var fileStream = new FileStream(fullPath,FileMode.Create))
                {
                    model.ImageUrl.CopyTo(fileStream);
                }
            }
            var blo = new Blog
            {
                ContentName = model.ContentName,
                Title = model.Title,
                Body = model.Body,
                ImageUrl = imageName
                
            };

            var result = await _blogRepository.CreateAsync(blo);
            return new BaseResponse
            {
                Message = "Blog Created Successfully",
                Success = true,
            };
        }
    
        public async Task<BlogsResponseModel> GetBlogsByDateAsync(DateTime Date)
        {
            var blog = await _blogRepository.GetBlogsByDateAsync(Date);
            if (blog == null)
            {
                return new BlogsResponseModel
                {
                    Message = $"No Blogs found for {Date}",
                    Success = false,
                };
                
            }
            return new BlogsResponseModel
            {
                Data = blog.Select(blo => new BlogDto
                {
                    ContentName = blo.ContentName,
                    Title = blo.Title,
                    Body = blo.Body,
                    CreatedOn = blo.CreatedOn,
                    ImageUrl = blo.ImageUrl, 
                }).ToList(),
                Message = "Assets found successfully",
                Success = true,

            };

        }
        public async Task<BlogResponseModel> GetBlogById(int id)
        {
            var blog = await _blogRepository.GetAsync(id);
            if (blog == null)
            {
                return new BlogResponseModel
                {
                    Message = $"Blog not found",
                    Success = false,
                };
            }
            return new BlogResponseModel
            {
                Data = new BlogDto
                {
                    ContentName = blog.ContentName,
                    Title = blog.Title,
                    Body = blog.Body,
                    CreatedOn = blog.CreatedOn,
                    ImageUrl = blog.ImageUrl, 
                },
                Message = "Blog found successfully",
                Success = true,

            };
        }
        public async Task<BaseResponse> DeleteBlogAsync(int id)
        {
            var blog = await _blogRepository.GetAsync(x => x.Id == id && x.IsDeleted == false);
            if (blog == null)
            {
                return new BaseResponse()
                {
                    Message = "No Blog found",
                    Success = false,
                };
            }
            blog.IsDeleted = true;
            await _blogRepository.UpdateAsync(blog);
            return new BaseResponse()
            {
                Message = "Blog Deletion Successful",
                Success = true
            };
        }
        public async Task<BlogsResponseModel> GetBlogsToDisplayAsync()
        {
            var blogToDisplay = await _blogRepository.GetBlogsToDisplayAsync();
            if (blogToDisplay.Count == 0)
            {
                return new BlogsResponseModel
                {
                    Message = "No Blog available",
                    Success = false
                };
            }
            return new BlogsResponseModel
            {
                Data = blogToDisplay.Select( a => new BlogDto
                {
                    ContentName = a.ContentName,
                    Title = a.Title,
                    Body = a.Body,
                    CreatedOn = a.CreatedOn,
                    ImageUrl = a.ImageUrl, 
                }).ToList(),
            };
        }

        public async Task<BaseResponse> UpdateBlogAsync(UpdateBlogRequestModels model)
        {
            var blog = await _blogRepository.GetAsync(x => x.Id == model.BlogId && x.IsDeleted == false);
            if (blog == null)
            {
                return new BaseResponse()
                {
                    Message = "No Blog found",
                    Success = false,
                };
            }
             if (model.ImageUrl != null)
            {
                IFormFile imageFile;
                SaveImage(model.ImageUrl, out imageFile);
                if (imageFile != null)
                {
                    model.ImageUrl = imageFile;
                }
            }
            blog.ContentName = model.ContentName;
            blog.Title = model.Title;
            blog.Body = model.Body;
            blog.CreatedOn = DateTime.Now;
            blog.ImageUrl = model.ImageUrl.FileName;
            await _blogRepository.UpdateAsync(blog);
            return new BaseResponse()
            {
                Message = "Blog Update Successful",
                Success = true
            };
        }

        public async Task<BlogResponseModel> GetBlogByContentName(string contentName)
        {
            var blog = await _blogRepository.GetAsync(x => x.ContentName == contentName && x.IsDeleted == false);
            if (blog == null)
            {
                return new BlogResponseModel
                {
                    Message = $"Blog not found",
                    Success = false,
                };
            }
            return new BlogResponseModel
            {
                Data = new BlogDto
                {
                    ContentName = blog.ContentName,
                    Title = blog.Title,
                    Body = blog.Body,
                    CreatedOn = blog.CreatedOn,
                    ImageUrl = blog.ImageUrl, 
                },
                Message = "Blog found successfully",
                Success = true,

            };
        }

        public async Task<BlogsResponseModel> GetAllBlogsAsync()
        {
            var blog = await _blogRepository.GetAllAsync();
            if (blog == null)
            {
                return new BlogsResponseModel
                {
                    Message = $"No Blogs found",
                    Success = false,
                };
            }
            return new BlogsResponseModel
            {
                Data = blog.Select(blo => new BlogDto
                {
                    ContentName = blo.ContentName,
                    Title = blo.Title,
                    Body = blo.Body,
                    CreatedOn = blo.CreatedOn,
                    ImageUrl = blo.ImageUrl, 
                }).ToList(),
                Message = "Blog found successfully",
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
            savedImageFile = new FormFile(memoryStream, 0, memoryStream.Length, null, Path.GetFileName(memoryStream.ToString()))
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/" + imageType
            };
        }
    }
}








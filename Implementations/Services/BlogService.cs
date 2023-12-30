using CMS_appBackend.Identity;
using CMS_appBackend.DTOs.ResponseModels;
using CMS_appBackend.Interface.Services;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
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
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IImageRepository _imageRepository;
        private readonly CloudinaryService _cloudinaryService;

        public BlogService(
            IBlogRepository repository,
            IPostRepository postRepository,
            IWebHostEnvironment webHostEnvironment,
            CloudinaryService cloudinaryService,
            IImageRepository imageRepository
        )
        {
            _blogRepository = repository;
            _postRepository = postRepository;
            _webHostEnvironment = webHostEnvironment;
            _cloudinaryService = cloudinaryService;
            _imageRepository = imageRepository;
        }

        public async Task<BaseResponse> CreateBlogAsync(CreateBlogRequestModel model)
        {
            var blog = await _blogRepository.GetAsync(
                x => x.Title == model.Title && x.IsDeleted == false
            );

            if (blog != null)
            {
                return new BaseResponse() { Message = "Blog Already Exists", Success = false };
            }

            string cloudinaryUrl = null;

            if (model.ImageUrl != null)
            {
                cloudinaryUrl = await _cloudinaryService.UploadImageToCloudinaryAsync(
                    model.ImageUrl
                );
            }
            var blo = new Blog
            {
                Title = model.Title,
                ImageUrl = cloudinaryUrl,
                CreatedOn = DateTime.UtcNow,
                Desccription = model.Desccription,
            };

            var result = await _blogRepository.CreateAsync(blo);

            return new BaseResponse { Message = "Blog Created Successfully", Success = true };
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
                Data = blog.Select(
                        blo =>
                            new BlogDto
                            {
                                Title = blo.Title,
                                ImageUrl = blo.ImageUrl,
                                CreatedOn = blo.CreatedOn.ToString("yyyy-MM-dd HH:mm:ss"), 
                                Desccription = blo.Desccription,
                            }
                    )
                    .ToList(),
                Message = "Assets found successfully",
                Success = true,
            };
        }

        public async Task<BlogResponseModel> GetBlogById(int id)
        {
            var blog = await _blogRepository.GetAsync(x => x.Id == id && x.IsDeleted == false);
            if (blog == null)
            {
                return new BlogResponseModel { Message = $"Blog not found", Success = false, };
            }
            return new BlogResponseModel
            {
                Data = new BlogDto
                {
                    Id = blog.Id,
                    Title = blog.Title,
                    ImageUrl = blog.ImageUrl,
                    CreatedOn = blog.CreatedOn.ToString("yyyy-MM-dd HH:mm:ss"),
                    Desccription = blog.Desccription,
                },
                Message = "Blog found successfully",
                Success = true,
            };
        }

        public async Task<BaseResponse> DeleteBlogAsync(int id)
        {
            var blog = await _blogRepository.GetBlogByIdAsync(id);
            if (blog == null)
            {
                return new BaseResponse() { Message = "No Blog found", Success = false, };
            }
            blog.IsDeleted = true;
            blog.DeletedOn = DateTime.Now;
            await _blogRepository.UpdateAsync(blog);
            return new BaseResponse() { Message = "Blog Deletion Successful", Success = true };
        }

        public async Task<BlogsResponseModel> GetBlogsToDisplayAsync()
        {
            var blogToDisplay = await _blogRepository.GetBlogsToDisplayAsync();
            if (blogToDisplay.Count == 0 || blogToDisplay == null)
            {
                return new BlogsResponseModel { Message = "No Blog available", Success = false };
            }
            return new BlogsResponseModel
            {
                Data = blogToDisplay
                    .Select(
                        a =>
                            new BlogDto
                            {
                                Id = a.Id,
                                Title = a.Title,
                                ImageUrl = a.ImageUrl,
                                CreatedOn = a.CreatedOn.ToString("yyyy-MM-dd HH:mm:ss"),
                                Desccription = a.Desccription,
                            }
                    )
                    .ToList(),
                Message = "Blog found successfully",
                Success = true,
            };
        }

        public async Task<BaseResponse> UpdateBlogAsync(UpdateBlogRequestModels model)
        {
            var blog = await _blogRepository.GetAsync(
                x => x.Id == model.BlogId && x.IsDeleted == false
            );
            if (blog == null)
            {
                return new BaseResponse() { Message = "No Blog found", Success = false, };
            }
            if (model.ImageUrl != null)
            {
                var cloudinaryImageUrl = await _cloudinaryService.UploadImageToCloudinaryAsync(
                    model.ImageUrl
                );
                blog.ImageUrl = cloudinaryImageUrl;
            }
            blog.Title = model.Title;
            blog.CreatedOn = DateTime.UtcNow;
            blog.Desccription = model.Desccription;
            await _blogRepository.UpdateAsync(blog);
            return new BaseResponse() { Message = "Blog Update Successful", Success = true };
        }

        public async Task<BlogResponseModel> GetBlogByTittle(GetBlogByTitleRequestModel model)
        {
            var blog = await _blogRepository.GetBlogByTitleAsync(model.Title);
            if (blog == null)
            {
                return new BlogResponseModel { Message = $"Blog not found", Success = false, };
            }
            return new BlogResponseModel
            {
                Data = new BlogDto
                {
                    Title = blog.Title,
                    ImageUrl = blog.ImageUrl,
                    CreatedOn = blog.CreatedOn.ToString("yyyy-MM-dd HH:mm:ss"),
                    Desccription = blog.Desccription,
                },
                Message = "Blog found successfully",
                Success = true,
            };
        }

        public async Task<BlogsResponseModel> GetAllBlogsAsync()
        {
            var blogs = await _blogRepository.GetAllBlogsAsync();
            if (blogs == null)
            {
                return new BlogsResponseModel { Message = "No Blogs found", Success = false };
            }

            var blogDtos = blogs
                .Select(
                    blo =>
                        new BlogDto
                        {
                            Id = blo.Id,
                            Title = blo.Title,
                            ImageUrl = blo.ImageUrl,
                            CreatedOn = blo.CreatedOn.ToString("yyyy-MM-dd HH:mm:ss"), // Adjust the format as needed
                            Desccription = blo.Desccription,
                        }
                )
                .ToList();

            return new BlogsResponseModel
            {
                Data = blogDtos,
                Message = "Blogs found successfully",
                Success = true,
            };
        }

        // private string GetImageUrlFromPublicId(string publicId)
        // {
        //     return _cloudinaryService.GetImageUrl(publicId);
        // }
    }
}

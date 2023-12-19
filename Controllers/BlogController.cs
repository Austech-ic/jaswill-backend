using CMS_appBackend.DTOs.RequestModels;
using CMS_appBackend.Interface.Repositories;
using CMS_appBackend.Interface.Services;
using Microsoft.AspNetCore.Mvc;
using CMS_appBackend.Implementations.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

using System.Diagnostics;
using System.Security.Claims;

namespace CMS_appBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly CloudinaryService _cloudinaryService;

        public BlogController(IBlogService blogService, CloudinaryService cloudinaryService)
        {
            _blogService = blogService;
            _cloudinaryService = cloudinaryService;
        }

        [HttpPost("CreateBlog")]
        public async Task<IActionResult> CreateBlog([FromForm] CreateBlogRequestModel model)
        {
            if (model.ImageUrl != null && model.ImageUrl.Length > 0)
            {
                var cloudinaryImageUrl = await _cloudinaryService.UploadImageToCloudinaryAsync(
                    model.ImageUrl
                );
            }
            var blog = await _blogService.CreateBlogAsync(model);

            if (blog.Success)
            {
                return Content(blog.Message);
            }

            return Content(blog.Message);
        }

        [HttpGet("GetBlog/{Id}")]
        public async Task<IActionResult> GetBlog(int Id)
        {
            var blog = await _blogService.GetBlogById(Id);
            if (blog.Success == true)
            {
                return Ok(blog);
            }
            return BadRequest(blog);
        }

        [HttpPut("UpdateBlog")]
        public async Task<IActionResult> UpdateBlog([FromForm] UpdateBlogRequestModels model)
        {
            if (model.ImageUrl != null && model.ImageUrl.Length > 0)
            {
                var cloudinaryImageUrl = await _cloudinaryService.UploadImageToCloudinaryAsync(
                    model.ImageUrl
                );
            }
            var blog = await _blogService.UpdateBlogAsync(model);
            if (blog.Success == true)
            {
                return Content(blog.Message);
            }
            return Content(blog.Message);
        }

        [HttpGet("GetBlogsToDisplayAsync")]
        public async Task<IActionResult> GetBlogsToDisplayAsync()
        {
            var blog = await _blogService.GetBlogsToDisplayAsync();
            if (blog.Success)
            {
                return Ok(blog.Message);
            }
            else
            {
                return BadRequest(blog.Message);
            }
        }

        [HttpDelete("DeleteBlog")]
        public async Task<IActionResult> DeleteBlog(int Id)
        {
            var blog = await _blogService.DeleteBlogAsync(Id);
            if (blog.Success == true)
            {
                return Content(blog.Message);
            }
            return Content(blog.Message);
        }

        [HttpGet("GetAllBlogsAsync")]
        public async Task<IActionResult> GetAllBlogsAsync()
        {
            var blog = await _blogService.GetAllBlogsAsync();
            if (blog.Success == true)
            {
                return Ok(blog);
            }
            return BadRequest(blog);
        }

        // [HttpPost("upload")]
        // public async Task<IActionResult> UploadImage([FromForm] IFormFile file)
        // {
        //     var imageUrl = await _cloudinaryService.UploadImageToCloudinaryAsync(file);
        //     if (imageUrl != null)
        //     {
        //         return Ok(imageUrl);
        //     }
        //     return BadRequest("Image Upload Failed");
        // }
    }
}

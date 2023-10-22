using CMS_appBackend.DTOs.RequestModels;
using CMS_appBackend.Interface.Repositories;
using CMS_appBackend.Interface.Services;
using Microsoft.AspNetCore.Mvc;
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

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpPost("CreateBlog")]
        public async Task<IActionResult> CreateBlog(CreateBlogRequestModel model)
        {
            var blog = await _blogService.CreateBlogAsync(model);
            if (blog.Success == true)
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
                return Content(blog.Message);
            }
            return Content(blog.Message);
        }

        [HttpPut("UpdateBlog")]
        public async Task<IActionResult> UpdateBlog(UpdateBlogRequestModels model)
        {
            var blog = await _blogService.UpdateBlogAsync(model);
            if (blog.Success == true)
            {
                return Content(blog.Message);
            }
            return Content(blog.Message);
        }

        [HttpGet("GetBlogsToDisplay")]
        public async Task<IActionResult> GetBlogsToDisplayAsync()
        {
            var blog = await _blogService.GetBlogsToDisplayAsync();
            if (blog.Success == true)
            {
                return Content(blog.Message);
            }
            return Content(blog.Message);
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

        [HttpGet("GetBlogsByDateAsync")]
        public async Task<IActionResult> GetBlogsByDateAsync(DateTime Date)
        {
            var blog = await _blogService.GetBlogsByDateAsync(Date);
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
                return Content(blog.Message);
            }
            return Content(blog.Message);
        }

        [HttpGet("GetBlogByContentName")]
        public async Task<IActionResult> GetBlogByContentName(string ContentName)
        {
            var blog = await _blogService.GetBlogByContentName(ContentName);
            if (blog.Success == true)
            {
                return Content(blog.Message);
            }
            return Content(blog.Message);
        }
    }
}

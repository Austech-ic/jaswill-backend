using CMS_appBackend.DTOs;
using CMS_appBackend.Models;
using Microsoft.AspNetCore.Mvc;
using CMS_appBackend.Interface.Services;
using CMS_appBackend.DTOs.RequestModels;
using System.Security.Claims;
using System.Web;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace CMS_appBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : Controller
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost("CreatePost")]
        public async Task<IActionResult> CreatePost(CreatePostRequestModel model)
        {
            var post = await _postService.CreatePostAsync(model);
            if (post.Success == true)
            {
                return Content(post.Message);
            }
            return Content(post.Message);
        }

        [HttpPost("UpdatePost")]
        public async Task<IActionResult> UpdatePost(UpdatePostRequestModel model)
        {
            var post = await _postService.UpdatePost(model);
            if (post.Success == true)
            {
                return Content(post.Message);
            }
            return Content(post.Message);
        }

        [HttpGet("GetPostById")]
        public async Task<IActionResult> GetPostById(int id)
        {
            var bidding = await _postService.GetPostByIdAsync(id);
            if (bidding.Success == true)
            {
                return View(bidding);
            }
            return Content(bidding.Message);
        }

        [HttpDelete("DeletePost")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _postService.DeletePost(id);
            if (post.Success == true)
            {
                return Content(post.Message);
            }
            return Content(post.Message);
        }

        [HttpGet("GetAllPosts")]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await _postService.GetAllPost();
            if (posts.Success == true)
            {
                return View(posts);
            }
            return Content(posts.Message);
        }
    }
}

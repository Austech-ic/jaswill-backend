using CMS_appBackend.DTOs;
using CMS_appBackend.Models;
using Microsoft.AspNetCore.Mvc;
using CMS_appBackend.Interface.Services;
using CMS_appBackend.DTOs.RequestModels;
using CMS_appBackend.Implementations.Services;
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
        private readonly CloudinaryService _cloudinaryService;

        public PostController(IPostService postService, CloudinaryService cloudinaryService)
        {
            _postService = postService;
            _cloudinaryService = cloudinaryService;
        }

        [HttpPost("CreatePost")]
        public async Task<IActionResult> CreatePost([FromForm]CreatePostRequestModel model)
        {
             if (model.PostImage != null && model.PostImage.Length > 0)
            {
                var cloudinaryImageUrl = await _cloudinaryService.UploadImageToCloudinaryAsync(
                    model.PostImage
                );
            }
            var post = await _postService.CreatePostAsync(model);
            if (post.Success == true)
            {
                return Content(post.Message);
            }
            return Content(post.Message);
        }

        [HttpPost("UpdatePost")]
        public async Task<IActionResult> UpdatePost([FromForm]UpdatePostRequestModel model)
        {
            if (model.PostImage != null && model.PostImage.Length > 0)
            {
                var cloudinaryImageUrl = await _cloudinaryService.UploadImageToCloudinaryAsync(
                    model.PostImage
                );
            }
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
            var post = await _postService.GetPostByIdAsync(id);
            if (post.Success == true)
            {
                return Ok(post);
            }
            return BadRequest(post);
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
                return Ok(posts);
            }
            return BadRequest(posts);
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

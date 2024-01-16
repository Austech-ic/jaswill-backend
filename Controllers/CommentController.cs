using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CMS_appBackend.DTOs.RequestModel;
using CMS_appBackend.Interface.Services;

namespace CMS_appBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CommentController(ICommentService commentService, IWebHostEnvironment webHostEnvironment)
        {
            _commentService = commentService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost("CreateComment")]
        public async Task<IActionResult> CreateComment([FromForm] CreateCommentRequestModel model)
        {
            var create = await _commentService.CreateComment(model);
            if (create.Success == false)
            {
                return BadRequest(create);
            }
            return Ok(create);
        }

        

        [HttpPut("UpdateComment/{id}")]
        public async Task<IActionResult> UpdateComment([FromForm] UpdateCommentRequestModel model, [FromRoute]int id)
        {
            var update = await _commentService.UpdateComment(model, id);
            if(update.Success == false)
            {
                return BadRequest(update);
            }
            return Ok(update);
        }

        [HttpGet("GetAllComment")]
        public async Task<IActionResult> GetAllComment()
        {
            var all = await _commentService.GetAll();
            if (all.Success == false)
            {
                return BadRequest(all);
            }
            return Ok(all);
        }

        [HttpGet("GetComment/{id}")]
        public async Task<IActionResult> GetComment([FromRoute] int id)
        {
            var comment = await _commentService.GetComment(id);
            if (comment.Success == false)
            {
                return BadRequest(comment);
            }
            return Ok(comment);
        }

        [HttpDelete("DeleteComment/{id}")]
        public async Task<IActionResult> DeleteComment([FromRoute] int id)
        {
            var delete = await _commentService.DeleteComment(id);
            if (delete.Success == false)
            {
                return BadRequest(delete);
            }
            return Ok(delete);
        }
                

        [HttpGet("GetCommentsByContent/{content}")]
        public async Task<IActionResult> GetCommentsByContent([FromRoute]string content)
        {
            var comments = await _commentService.GetCommentsByContent(content);
            if(comments.Success == false)
            {
                return BadRequest(comments);
            }
            return Ok(comments);
        }
    }
}

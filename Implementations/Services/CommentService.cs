using CMS_appBackend.DTOs;
using CMS_appBackend.DTOs.RequestModel;
using CMS_appBackend.DTOs.ResponseModels;
using CMS_appBackend.Entities;
using CMS_appBackend.Interface.Repositories;
using CMS_appBackend.Interface.Services;

namespace CMS_appBackend.Implementations.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBlogRepository _blogRepository;

        public CommentService(ICommentRepository commentRepository, IUserRepository userRepository, IBlogRepository blogRepository)
        {
            _commentRepository = commentRepository;
            _userRepository = userRepository;
            _blogRepository = blogRepository;
        }

        public async Task<BaseResponse> CreateComment(CreateCommentRequestModel model)
        {
            var user = await _commentRepository.GetAsync(x => x.CommentInput == model.CommentInput && x.IsDeleted == false);
            if (model.CommentInput == null)
            {
                return new BaseResponse
                {
                    Message = "Field cannot be empty",
                    Success = false
                };
            }

            var comment = new Comment
            {
                CommentInput = model.CommentInput,
                UserName = model.UserName,
            };
            await _commentRepository.CreateAsync(comment);
            return new BaseResponse
            {
                Message = "Comment posted successfully",
                Success = true
            };

        }

        public async Task<CommentsResponseModel> GetAll()
        {
            var list = await _commentRepository.GetAll();
            if(list == null)
            {
                return new CommentsResponseModel
                {
                    Message = "No available comment",
                    Success = false
                };
            }
            var comments = list.Where(x => x.IsDeleted == false).Select(x => new CommentDTO
            {
                Id = x.Id,
                CommentInput = x.CommentInput,
                UserName = x.UserName,                
            }).ToList();
            return new CommentsResponseModel
            {
                Data = comments.ToHashSet(),
                Message = "List of comments",
                Success = true
            };
        }

        public async Task<CommentResponseModel> GetComment(int id)
        {
            var comment = await _commentRepository.GetComment(id);
            if (comment == null)
            {
                return new CommentResponseModel
                {
                    Message = "Comment not available",
                    Success = false
                };
            }
            var commentDto = new CommentDTO
            {
                CommentInput = comment.CommentInput,
                Id = comment.Id,
                UserName = comment.UserName,
            };
            return new CommentResponseModel
            {
                Data = commentDto,
                Message = "",
                Success = true
            };
        }

        public async Task<BaseResponse> DeleteComment(int id)
        {
            var comment = await _commentRepository.GetAsync(x => x.Id == id);
            if (comment == null)
            {
                return new BaseResponse
                {
                    Message = "Comment not found",
                    Success = false
                };
            }
            comment.IsDeleted = true;
            comment.LastModifiedOn = DateTime.Now;
            await _commentRepository.UpdateAsync(comment);
            return new BaseResponse
            {
                Message = "Comment deleted successfully",
                Success = true
            };
        }


        public async Task<CommentsResponseModel> GetCommentsByContent(string content)
        {
            var list = await _commentRepository.GetCommentsByContent(content);
            var comments = list.Where(x => x.IsDeleted == false).Select(x => new CommentDTO
            {
                CommentInput = x.CommentInput,
                Id = x.Id,
            }).ToHashSet();
            if (comments.Count == 0)
            {
                return new CommentsResponseModel
                {
                    Message = "No comment found",
                    Success = false
                };
            }
            return new CommentsResponseModel
            {
                Data = comments,
                Message = "",
                Success = true
            };
        }

        public async Task<BaseResponse> UpdateComment(UpdateCommentRequestModel model, int id)
        {
            var comment = await _commentRepository.GetAsync(x => x.Id == id);
            if (comment == null)
            {
                return new BaseResponse
                {
                    Message = "Comment not found",
                    Success = false
                };
            }
            comment.CommentInput = model.CommentInput;
            comment.UserName = model.UserName;
            comment.LastModifiedOn = DateTime.UtcNow;
            var comm = await _commentRepository.UpdateAsync(comment);
            return new BaseResponse
            {
                Message = "Comment updated successfully",
                Success = true
            };
        }
    }
}

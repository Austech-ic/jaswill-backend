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

        public async Task<BaseResponse> CreateComment(CreateCommentRequestModel model, int userId, int blogId)
        {
            var user = await _userRepository.GetAsync(x => x.Id == userId);
            if (user == null)
            {
                return new BaseResponse
                {
                    Message = "User not found",
                    Success = false
                };
            }

            if (model.Detail == null)
            {
                return new BaseResponse
                {
                    Message = "Field cannot be empty",
                    Success = false
                };
            }
            
            var blog = await _blogRepository.GetAsync(x => x.Id == blogId);
            if (blog == null)
            {
                return new BaseResponse
                {
                    Message = "Blog not found",
                    Success = false
                };
            }

            var comment = new Comment
            {
                Detail = model.Detail,
                Blog = blog,
                BlogId = blogId,
                User = user,
                UserId = user.Id,
                CreatedBy = user.Id,
                LastModifiedBy = user.Id,
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
                Detail = x.Detail,
                BlogerName  = x.Blog.ContentName,
                UserName  = $"{x.User.Email}",
                
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
                Detail = comment.Detail,
                UserName = $"{comment.User.Email}",
                BlogerName = comment.Blog.ContentName,
                Id = comment.Id,
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


        public async Task<CommentsResponseModel> GetCommentByBlogId(int id)
        {
            var comments = await _commentRepository.GetCommentByBlogId(id);
            var result = comments.Where(x => x.IsDeleted == false).Select(x => new CommentDTO
            {
                Detail = x.Detail,
                BlogerName = x.Blog.ContentName,
                UserName = $"{x.User.Email}",
                Id = x.Id,
            }).ToHashSet();
            if(result.Count == 0)
            {
                return new CommentsResponseModel
                {
                    Message = "No comment found",
                    Success = false
                };
            }
            return new CommentsResponseModel
            {
                Data = result,
                Message = "",
                Success = true
            };
        }

        public async Task<CommentsResponseModel> GetCommentsByContent(string content)
        {
            var list = await _commentRepository.GetCommentsByContent(content);
            var comments = list.Where(x => x.IsDeleted == false).Select(x => new CommentDTO
            {
                Detail = x.Detail,
                BlogerName = x.Blog.ContentName,
                UserName = $"{x.User.Email}",
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
            comment.Detail = model.Detail;
            comment.LastModifiedOn = DateTime.Now;
            var comm = await _commentRepository.UpdateAsync(comment);
            return new BaseResponse
            {
                Message = "Comment updated successfully",
                Success = true
            };
        }
    }
}

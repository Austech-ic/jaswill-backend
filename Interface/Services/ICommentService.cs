using CMS_appBackend.DTOs.RequestModel;
using CMS_appBackend.DTOs.ResponseModels;

namespace CMS_appBackend.Interface.Services
{
    public interface ICommentService
    {
        Task<BaseResponse> CreateComment(CreateCommentRequestModel model);
        Task<BaseResponse> UpdateComment(UpdateCommentRequestModel model, int id);
        Task<CommentsResponseModel> GetCommentsByContent(string content);
        Task<CommentsResponseModel> GetAll();
        Task<CommentResponseModel> GetComment(int id);
        Task<BaseResponse> DeleteComment(int id);
    }
}

using CMS_appBackend.Entities.Identity;
using CMS_appBackend.DTOs.ResponseModels;
using CMS_appBackend.DTOs.RequestModels;

namespace CMS_appBackend.Interface.Services
{
    public interface IPostService
    {
        // Task<BaseResponse> ChangeAuctionOpeningDateAsync(int id, DateTime ExpiryDate);
        Task<BaseResponse> CreatePostAsync(CreatePostRequestModel model);
        Task<BaseResponse> UpdatePost(UpdatePostRequestModel model);
        Task<BaseResponse> GetPostByIdAsync(int id);
        Task<BaseResponse> DeletePost(int id);
        Task<PostsResponseModel> GetAllPost();
    }
}
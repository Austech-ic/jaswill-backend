using CMS_appBackend.Identity;
using CMS_appBackend.DTOs.ResponseModels;
using CMS_appBackend.DTOs.RequestModels;

namespace CMS_appBackend.Interface.Services
{
    public interface IPostService
    {
        // Task<BaseResponse> ChangeAuctionOpeningDateAsync(int id, DateTime ExpiryDate);
        Task<BaseResponse> CreatePostAsync(CreatePostRequestModel model, IFormFile PostImage);
        Task<BaseResponse> UpdatePost(UpdatePostRequestModel model, IFormFile PostImage);
        Task<PostResponseModel> GetPostByIdAsync(int id);
        Task<BaseResponse> DeletePost(int id);
        Task<PostsResponseModel> GetAllPost();
    }
}
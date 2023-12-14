using CMS_appBackend.Identity;
using CMS_appBackend.DTOs.ResponseModels;
using CMS_appBackend.DTOs.RequestModels;

namespace CMS_appBackend.Interface.Services
{
    public interface IBlogService
    {
        Task<BaseResponse> CreateBlogAsync(CreateBlogRequestModel model);
        Task<BlogsResponseModel> GetBlogsByDateAsync(DateTime Date);
        Task<BaseResponse> DeleteBlogAsync(int id);
        Task<BlogsResponseModel> GetBlogsToDisplayAsync();
        // Task<BaseResponse> AddBlogForAuctionAsync(int id);
        Task<BlogResponseModel> GetBlogById(int id);
        Task<BaseResponse> UpdateBlogAsync(UpdateBlogRequestModels model);
        Task<BlogsResponseModel> GetAllBlogsAsync();
        Task<BlogResponseModel> GetBlogByTittle(string title);

    }
}

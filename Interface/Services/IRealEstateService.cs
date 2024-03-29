using CMS_appBackend.Identity;
using CMS_appBackend.DTOs.ResponseModels;
using CMS_appBackend.DTOs.RequestModels;

namespace CMS_appBackend.Interface.Services
{
    public interface IRealEstateService
    {
        Task<BaseResponse> CreateRealEstateAsync(CreateRealEstateRequestModel model);
        Task<RealEstateResponseModel> GetRealEstateByIdAsync(int id);
        Task<BaseResponse> DeleteRealEstateAsync(int id);
        Task<BaseResponse> UpdateRealEstateAsync(UpdateRealEstateRequstModel model);
        Task<RealEstatesResponseModel> GetAllRealEstatesAsync();
        Task<RealEstatesResponseModel> GetRealEstatesByCategoryIdAsync(int id);
        Task<RealEstatesResponseModel> GetRealEstatesByTypeAsync(GetRealEstatesByTypeRequestModel model);
        Task<RealEstatesResponseModel> GetRealEstateByCategoryNameAsync(GetRealEstatesByCategoryNameRequestModel model);
        Task<RealEstatesResponseModel> GetRealEstateByCategoryIdAsync(int id);
        Task<RealEstatesResponseModel> GetAllRealEstatesByCategoriesAsync();
    }
}

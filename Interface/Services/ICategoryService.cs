using CMS_appBackend.DTOs.RequestModel;
using CMS_appBackend.DTOs.ResponseModels;

namespace CMS_appBackend.Interface.Services
{
    public interface ICategoryService
    {
        Task<BaseResponse> AddCategory(CreateCategoryRequestModel model);
        Task<BaseResponse> UpdateCategory(UpdateCategoryRequestModel model, int id);
        Task<CategoriesResponseModel> GetAll();
        Task<CategoryResponseModel> GetById(int id);
        Task<CategoriesResponseModel> GetCategoriesByName(string name);
        Task<CategoriesResponseModel> GetAllWithInfo();
    }
}

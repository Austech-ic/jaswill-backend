﻿using CMS_appBackend.DTOs.RequestModel;
using CMS_appBackend.DTOs.ResponseModels;

namespace CMS_appBackend.Interface.Services
{
    public interface ICategoryService
    {
        Task<BaseResponse> CreateCategory(CreateCategoryRequestModel model);
        Task<BaseResponse> UpdateCategory(UpdateCategoryRequestModel model);
        Task<CategoriesResponseModel> GetAll();
        Task<CategoryResponseModel> GetById(int id);
        Task<CategoriesResponseModel> GetCategoriesByName(GetCategoriesByNameRequestModel model);
        Task<CategoriesResponseModel> GetAllWithInfo();
    }
}

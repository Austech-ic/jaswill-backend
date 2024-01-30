using CMS_appBackend.DTOs;
using CMS_appBackend.DTOs.RequestModel;
using CMS_appBackend.DTOs.ResponseModels;
using CMS_appBackend.Entities;
using CMS_appBackend.Interface.Repositories;
using CMS_appBackend.Interface.Services;

namespace CMS_appBackend.Implementations.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IImageService _imageService;

        private readonly CloudinaryService _cloudinaryService;

        public CategoryService(
            ICategoryRepository categoryRepository,
            IImageService imageService,
            CloudinaryService cloudinaryService
        )
        {
            _categoryRepository = categoryRepository;
            _imageService = imageService;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<BaseResponse> CreateCategory(CreateCategoryRequestModel model)
        {
            var check = await _categoryRepository.GetAsync(
                x => x.CategoryName == model.CategoryName && x.IsDeleted == false
            );
            if (check != null)
            {
                return new BaseResponse { Message = "Category already existed!", Success = false };
            }
            string cloudinaryUrl = null;
            if (model.Image != null)
            {
                cloudinaryUrl = await _cloudinaryService.UploadImageToCloudinaryAsync(model.Image);
            }
            var category = new Category
            {
                CategoryName = model.CategoryName,
                Description = model.Description,
                Image = cloudinaryUrl,
                Title = model.Title,
                Amount = model.Amount
            };
            await _categoryRepository.CreateAsync(category);
            return new BaseResponse { Message = "Category added successfully!", Success = true };
        }

        public async Task<CategoriesResponseModel> GetAll()
        {
            var categories = await _categoryRepository.GetAll();
            if (categories == null)
            {
                return new CategoriesResponseModel
                {
                    Message = "No category found",
                    Success = false
                };
            }
            return new CategoriesResponseModel
            {
                Data = categories
                    .Select(
                        x =>
                            new CategoryDto
                            {
                                Id = x.Id,
                                CategoryName = x.CategoryName,
                                Description = x.Description,
                                Image = x.Image,
                                Title = x.Title,
                                Amount = x.Amount
                            }
                    )
                    .ToList(),
                Message = "List of Categories",
                Success = true
            };
        }

        

        public async Task<CategoryResponseModel> GetById(int id)
        {
            var category = await _categoryRepository.GetCategory(id);
            if (category == null)
            {
                return new CategoryResponseModel
                {
                    Message = "Category not found",
                    Success = false
                };
            }

            var categoryDTO = new CategoryDto
            {
                Id = category.Id,
                CategoryName = category.CategoryName,
                Description = category.Description,
                Amount = category.Amount,
                Image = category.Image,
                Title = category.Title
            };
            return new CategoryResponseModel
            {
                Message = "Category found",
                Data = categoryDTO,
                Success = true
            };
        }

        public async Task<CategoryResponseModel> GetCategoriesByName(
            GetCategoriesByNameRequestModel model
        )
        {
            var category = await _categoryRepository.GetCategoryByName( model.CategoryName);
            if (category == null)
            {
                return new CategoryResponseModel
                {
                    Message = "Category not found",
                    Success = false
                };
            }
            var categoryDTO = new CategoryDto
            {
                Id = category.Id,
                CategoryName = category.CategoryName,
                Description = category.Description,
                Amount = category.Amount,
                Image = category.Image,
                Title = category.Title
            };
            return new CategoryResponseModel
            {
                Message = "Category found",
                Data = categoryDTO,
                Success = true
            };
        }

        public async Task<BaseResponse> UpdateCategory(UpdateCategoryRequestModel model)
        {
            var category = await _categoryRepository.GetById(model.Id);
            if (category == null)
            {
                return new BaseResponse { Message = "Category not found", Success = false };
            }
            string cloudinaryUrl = null;
            if (model.Image != null)
            {
                cloudinaryUrl = await _cloudinaryService.UploadImageToCloudinaryAsync(model.Image);
            }
            category.CategoryName = model.CategoryName ?? category.CategoryName;
            category.Description = model.Description ?? category.Description;
            category.Title = model.Title ?? category.Title;
            category.Amount = model.Amount ?? category.Amount;
            category.Image = cloudinaryUrl ?? category.Image;
            await _categoryRepository.UpdateAsync(category);
            return new BaseResponse { Message = "Category updated succdessfully", Success = true };
        }

        public async Task<BaseResponse> DeleteCategory(int id)
        {
            var category = await _categoryRepository.GetById(id);
            if (category == null)
            {
                return new BaseResponse { Message = "Category not found", Success = false };
            }
            category.IsDeleted = true;
            await _categoryRepository.UpdateAsync(category);
            return new BaseResponse { Message = "Category deleted successfully", Success = true };
        }

        public async Task<CategoriesResponseModel> GetCategoriesToDisplay()
        {
            var categories = await _categoryRepository.GetCategoriesToDisplay();
            if (categories.Count == 0 || categories == null)
            {
                return new CategoriesResponseModel
                {
                    Message = "No category available",
                    Success = false
                };
            }
            return new CategoriesResponseModel
            {
                Data = categories
                    .Select(
                        a =>
                            new CategoryDto
                            {
                                Id = a.Id,
                                CategoryName = a.CategoryName,
                                Description = a.Description,
                                Image = a.Image,
                                Title = a.Title,
                                Amount = a.Amount
                            }
                    )
                    .ToList(),
                Message = "Categories found successfully",
                Success = true,
            };
        }
    }
}

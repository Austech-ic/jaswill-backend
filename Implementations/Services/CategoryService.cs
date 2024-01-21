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
        private readonly IBlogRepository _blogRepository;
        public CategoryService(ICategoryRepository categoryRepository, IBlogRepository blogRepository)
        {
            _categoryRepository = categoryRepository;
            _blogRepository = blogRepository;
        }
        public async Task<BaseResponse> CreateCategory(CreateCategoryRequestModel model)
        {
            var check = await _categoryRepository.GetAsync(x => x.CategoryName == model.CategoryName && x.IsDeleted == false);
            if (check != null)
            {
                return new BaseResponse
                {
                    Message = "Category already existed!",
                    Success = false
                };
            }
            var category = new Category
            {
                CategoryName = model.CategoryName,
                Description = model.Description,
            };
            await _categoryRepository.CreateAsync(category);
            return new BaseResponse
            {
                Message = "Category added successfully!",
                Success = true
            };
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
                Data = categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    CategoryName = x.CategoryName,
                    Description = x.Description,
                    RealEstateDtos = x.RealEstates.Select(n => new RealEstateDto
                    {
                        Id = n.Id,
                        ImageUrl = n.ImageUrl,
                        Title = n.Title,
                        Description = n.Description,
                        Type = n.Type,
                        City = n.City,
                        Price = n.Price,
                        Agency = n.Agency,
                        Agreement = n.Agreement,
                        Caution = n.Caution,
                        ServiceCharge = n.ServiceCharge,
                        Total = n.Total,
                        Propertylocation = n.Propertylocation,
                        NumberOfBedrooms = n.NumberOfBedrooms,
                        NumberOfFloors = n.NumberOfFloors,
                        NumberOfBathrooms = n.NumberOfBathrooms,
                        Content = n.Content,
                    }).ToList(),
                }).ToList(),
                Message = "List of Categories",
                Success = true
            };
        }

        public async Task<CategoriesResponseModel> GetAllWithInfo()
        {
            var categories = await _categoryRepository.GetAllWithInfo();
            if(categories == null)
            {
                return new CategoriesResponseModel
                {
                    Message = "No category found",
                    Success = false
                };
            }
            
            var dto =  categories.Select( x => new CategoryDto
            {
                Id = x.Id,
                CategoryName = x.CategoryName,
                Description = x.Description,
                RealEstateDtos = x.RealEstates.Select(n => new RealEstateDto
                {
                    Id = n.Id,
                    ImageUrl = n.ImageUrl,
                    Title = n.Title,
                    Description = n.Description,
                    Type = n.Type,
                    City = n.City,
                    Price = n.Price,
                    Agency = n.Agency,
                    Agreement = n.Agreement,
                    Caution = n.Caution,
                    ServiceCharge = n.ServiceCharge,
                    Total = n.Total,
                    Propertylocation = n.Propertylocation,
                    NumberOfBedrooms = n.NumberOfBedrooms,
                    NumberOfFloors = n.NumberOfFloors,
                    NumberOfBathrooms = n.NumberOfBathrooms,
                    Content = n.Content,
                }).ToList(),
            }).ToHashSet();

            return new CategoriesResponseModel
            {
                Data = dto,
                Message = "List of Categories with information",
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
                RealEstateDtos = category.RealEstates.Select(n => new RealEstateDto
                {
                    Id = n.Id,
                    ImageUrl = n.ImageUrl,
                    Title = n.Title,
                    Description = n.Description,
                    Type = n.Type,
                    City = n.City,
                    Price = n.Price,
                    Agency = n.Agency,
                    Agreement = n.Agreement,
                    Caution = n.Caution,
                    ServiceCharge = n.ServiceCharge,
                    Total = n.Total,
                    Propertylocation = n.Propertylocation,
                    NumberOfBedrooms = n.NumberOfBedrooms,
                    NumberOfFloors = n.NumberOfFloors,
                    NumberOfBathrooms = n.NumberOfBathrooms,
                    Content = n.Content,
                }).ToList(),
            };
            return new CategoryResponseModel
            {
                Message = "Category found",
                Data = categoryDTO,
                Success = true
            };
        }

        public async Task<CategoryResponseModel> GetCategoriesByName(GetCategoriesByNameRequestModel model)
        {
           var category = await _categoryRepository.GetAsync(x => x.CategoryName == model.Name && x.IsDeleted == false);
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
                RealEstateDtos = category.RealEstates.Select(n => new RealEstateDto
                {
                    Id = n.Id,
                    ImageUrl = n.ImageUrl,
                    Title = n.Title,
                    Description = n.Description,
                    Type = n.Type,
                    City = n.City,
                    Price = n.Price,
                    Agency = n.Agency,
                    Agreement = n.Agreement,
                    Caution = n.Caution,
                    ServiceCharge = n.ServiceCharge,
                    Total = n.Total,
                    Propertylocation = n.Propertylocation,
                    NumberOfBedrooms = n.NumberOfBedrooms,
                    NumberOfFloors = n.NumberOfFloors,
                    NumberOfBathrooms = n.NumberOfBathrooms,
                    Content = n.Content,
                }).ToList(),
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
                return new BaseResponse
                {
                    Message = "Category not found",
                    Success = false
                };
            }
            category.CategoryName = model.Name ?? category.CategoryName;
            category.Description = model.Description ?? category.Description;
            await _categoryRepository.UpdateAsync(category);
            return new BaseResponse
            {
                Message = "Category updated succdessfully",
                Success = true
            };
        }

        public async Task<BaseResponse> DeleteCategory(int id)
        {
            var category = await _categoryRepository.GetById(id);
            if (category == null)
            {
                return new BaseResponse
                {
                    Message = "Category not found",
                    Success = false
                };
            }
            category.IsDeleted = true;
            await _categoryRepository.UpdateAsync(category);
            return new BaseResponse
            {
                Message = "Category deleted successfully",
                Success = true
            };
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
                                RealEstateDtos = a.RealEstates.Select(n => new RealEstateDto
                                {
                                    Id = n.Id,
                                    ImageUrl = n.ImageUrl,
                                    Title = n.Title,
                                    Description = n.Description,
                                    Type = n.Type,
                                    City = n.City,
                                    Price = n.Price,
                                    Agency = n.Agency,
                                    Agreement = n.Agreement,
                                    Caution = n.Caution,
                                    ServiceCharge = n.ServiceCharge,
                                    Total = n.Total,
                                    Propertylocation = n.Propertylocation,
                                    NumberOfBedrooms = n.NumberOfBedrooms,
                                    NumberOfFloors = n.NumberOfFloors,
                                    NumberOfBathrooms = n.NumberOfBathrooms,
                                    Content = n.Content,
                                }).ToList(),
                            }
                    )
                    .ToList(),
                Message = "Categories found successfully",
                Success = true,
            };
        }
    }
}

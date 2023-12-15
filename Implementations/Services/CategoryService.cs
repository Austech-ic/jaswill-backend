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
            var check = await _categoryRepository.GetAsync(x => x.CategoryName == model.CategoryName);
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
            var all = await _categoryRepository.GetAll();
            var categories = all.Where(x => x.IsDeleted == false).Select(x => new CategoryDto
            {
                Id = x.Id,
                CategoryName = x.CategoryName,
                Blogs = x.Blogs.Select(n => new BlogDto
                {
                    Title = n.Title,
                    ImageUrl = n.ImageUrl,
                    BlogId = n.Id,
                }).ToList(),
            }).ToList();
            if (categories == null)
            {
                return new CategoriesResponseModel
                {
                    Message = "Categories not available",
                    Success = false
                };
            }
            return new CategoriesResponseModel
            {
                Message = "List of categories",
                Data = categories.ToHashSet(),
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
                Blogs = x.Blogs.Select(n => new BlogDto
                {
                    Title = n.Title,
                    ImageUrl = n.ImageUrl,
                    BlogId = n.Id,
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
                Blogs = category.Blogs.Select(n => new BlogDto
                {
                    Title = n.Title,
                    ImageUrl = n.ImageUrl,
                    BlogId = n.Id,
                }).ToList(),
            };
            return new CategoryResponseModel
            {
                Message = "Category found",
                Data = categoryDTO,
                Success = true
            };
        }

        public async Task<CategoriesResponseModel> GetCategoriesByName(GetCategoriesByNameRequestModel model)
        {
            var catg = await _categoryRepository.GetAll();
            var result = catg.Where(x => x.CategoryName.ToLower().Contains(model.Name.ToLower())).Select(x => new CategoryDto
            {
                Id = x.Id,
                CategoryName = x.CategoryName,
                Blogs = x.Blogs.Select(n => new BlogDto
                {
                    Title = n.Title,
                    ImageUrl = n.ImageUrl,
                    BlogId = n.Id,
                }).ToList(),
            }).ToList();
            if(result.Count == 0)
            {
                return new CategoriesResponseModel
                {
                    Message = "No category found",
                    Success = false
                };
            }
            return new CategoriesResponseModel
            {
                Data = result,
                Message = "List of categories",
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
            category.CategoryName = model.Name;
            await _categoryRepository.UpdateAsync(category);
            return new BaseResponse
            {
                Message = "Category updated succdessfully",
                Success = true
            };
        }
    }
}

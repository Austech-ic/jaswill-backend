namespace CMS_appBackend.DTOs.ResponseModels
{
    public class CategoryResponseModel : BaseResponse
    {
        public CategoryDto Data { get; set; }
    }
    public class CategoriesResponseModel : BaseResponse
    {
        public ICollection<CategoryDto> Data { get; set; } = new HashSet<CategoryDto>();

    }
}

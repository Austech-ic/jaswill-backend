using System.ComponentModel.DataAnnotations;

namespace CMS_appBackend.DTOs.RequestModel
{
    public class CreateCategoryRequestModel
    {
        public string CategoryName { get; set; }
        public IFormFile? Image { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Amount {get; set;}
    }

    public class UpdateCategoryRequestModel
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public IFormFile Image { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Amount {get; set;}
    }

    public class GetCategoriesByNameRequestModel
    {
        public string CategoryName { get; set; }
    }
}

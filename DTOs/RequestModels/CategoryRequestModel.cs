using System.ComponentModel.DataAnnotations;

namespace CMS_appBackend.DTOs.RequestModel
{
    public class CreateCategoryRequestModel
    {
        public string CategoryName { get; set; }
        public string Title {get; set;}
        public string Description {get; set;}
        public string Price {get; set;}
    }

    public class UpdateCategoryRequestModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title {get; set;}
        public string Description {get; set;}
        public string Price {get; set;}
    }

    public class GetCategoriesByNameRequestModel
    {
        public string Name { get; set; }
        public string Title {get; set;}
        public string Description {get; set;}
        public string Price {get; set;}
    }
}

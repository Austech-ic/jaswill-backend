using System.ComponentModel.DataAnnotations;

namespace CMS_appBackend.DTOs.RequestModel
{
    public class CreateCategoryRequestModel
    {
        public string CategoryName { get; set; }
    }

    public class UpdateCategoryRequestModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

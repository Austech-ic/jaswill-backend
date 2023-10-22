namespace CMS_appBackend.DTOs.RequestModels
{
    public class UpdatePostRequestModel
    {
        public string PostName {get; set;}
        public IFormFile PostImage {get; set;}
        public string Content {get; set;}
        public string CreatedBy {get; set;}
        public string PostTag {get; set;}
        public DateTime CreatedOn {get; set;}
        public bool IsDeleted {get; set;}
    }
}
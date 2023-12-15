namespace CMS_appBackend.DTOs.RequestModels
{
    public class UpdatePostRequestModel
    {
        public string PostName {get; set;}
        public IFormFile PostImage {get; set;}
        public string Content {get; set;}
        public string PostTag {get; set;}
    }
}
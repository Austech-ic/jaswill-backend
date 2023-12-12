namespace CMS_appBackend.DTOs.RequestModels
{
    public class CreateBlogRequestModel
    {
        public IFormFile ImageUrl { get; set; }
        public string Title {get;set;}
        public DateTime CreatedOn {get; set;}
    }

}
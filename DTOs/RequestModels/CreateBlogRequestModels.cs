namespace CMS_appBackend.DTOs.RequestModels
{
    public class CreateBlogRequestModel
    {
        public string ContentName { get; set; }
        public IFormFile ImageUrl { get; set; }
        public string Title {get;set;}
        public string Body { get; set; }
        public DateTime CreatedOn {get; set;}
        public string CreatedBy {get; set;}
    }

}
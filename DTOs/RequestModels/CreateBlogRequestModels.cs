using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;
namespace CMS_appBackend.DTOs.RequestModels
{
    public class CreateBlogRequestModel
    {
        public IFormFile ImageUrl { get; set; }
        public string Title {get;set;}
    }

    public class GetBlogByTitleRequestModel
    {
        public string Title {get;set;}
    }

}
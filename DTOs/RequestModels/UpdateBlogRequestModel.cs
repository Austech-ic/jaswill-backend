
using System;

namespace CMS_appBackend.DTOs.RequestModels
{

    public class UpdateBlogRequestModels
    {
        public IFormFile ImageUrl { get; set; }
        public string Title {get;set;}
        public string Desccription {get; set;}
        public string CreatedOn {get; set;}
        public int BlogId { get; set; }
    }
}
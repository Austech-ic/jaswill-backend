namespace CMS_appBackend.DTOs.RequestModels
{
    public class CreateRealEstateRequestModel
    {
        public string Title {get;set;}
        public string Description {get;set;}
        public string Address {get;set;}
        public IFormFile ImageUrl {get;set;}
        public string Price {get;set;}
        public string Features {get;set;}
    }
}
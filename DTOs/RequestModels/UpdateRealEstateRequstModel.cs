namespace CMS_appBackend.DTOs.RequestModels
{
    public class UpdateRealEstateRequstModel
    {
        public int Id {get;set;}
        public string Title {get;set;}
        public string Description {get;set;}
        public string Address {get;set;}
        public IFormFile ImageUrl {get;set;}
        public string Price {get;set;}
        public string Features {get;set;}
    }
}
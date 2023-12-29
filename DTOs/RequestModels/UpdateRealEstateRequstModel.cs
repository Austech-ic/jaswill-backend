namespace CMS_appBackend.DTOs.RequestModels
{
    public class UpdateRealEstateRequstModel
    {
        public int Id {get;set;}
        public string Title {get;set;}
        public string Description {get;set;}
        public string Content {get;set;}
        public string Type { get; set; }
        public string City { get; set; }
        public string Price { get; set; }
        public string Propertylocation { get; set; }
        public string NumberOfBedrooms { get; set; }
        public string NumberOfBathrooms { get; set; }
        public string NumberOfFloors { get; set; }
        public IFormFile ImageUrl {get;set;}
    }
}
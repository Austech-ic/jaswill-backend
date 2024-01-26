namespace CMS_appBackend.DTOs.RequestModels
{
    public class CreateRealEstateRequestModel
    {
        public string Title {get;set;}
        public int CategoryId {get; set;}

        public string Description {get;set;}
        public string Content {get;set;}
        public string Type { get; set; }
        public string City { get; set; }
        public string Price { get; set; }
        public string? Agency { get; set; }
        public string? Agreement { get; set; }
        public string? Caution { get; set; }
        public string? ServiceCharge { get; set; }
        public string? Total { get; set; }
        public string Propertylocation { get; set; }
        public string NumberOfBedrooms { get; set; }
        public string NumberOfBathrooms { get; set; }
        public string NumberOfFloors { get; set; }
        public IFormFile ImageUrl {get;set;}
    }

    public class GetRealEstatesByTypeRequestModel
    {
        public string Type { get; set; }
    }
}
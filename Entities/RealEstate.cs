using CMS_appBackend.Contracts;

namespace CMS_appBackend.Entities
{
    public class RealEstate : AuditableEntity
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Content {get;set;}
        public string? Type { get; set; }
        public string? City { get; set; }
        public string? Price { get; set; }
        public string? Agency { get; set; }
        public string? Agreement { get; set; }
        public string? Caution { get; set; }
        public string? ServiceCharge { get; set; }
        public string? Total { get; set; }
        public string? Propertylocation { get; set; }
        public string? NumberOfBedrooms { get; set; }
        public string? NumberOfBathrooms { get; set; }
        public string? NumberOfFloors { get; set; }
        public string? ImageUrl { get; set; }
        public int CategoryId {get; set;}
        public string CategoryName {get; set;}
        public Category Category {get; set;}
        public IList<Image> Images{get; set;} = new List<Image>();
    }
}
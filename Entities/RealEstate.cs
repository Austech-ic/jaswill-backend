using CMS_appBackend.Contracts;

namespace CMS_appBackend.Entities
{
    public class RealEstate : AuditableEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Address{get;set;}
        public string ImageUrl { get; set; }
        public string Price { get; set; }
        public string Features { get; set; }
        public IList<Image> Images{get; set;} = new List<Image>();
    }
}
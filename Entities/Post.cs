using CMS_appBackend.Contracts;

namespace CMS_appBackend.Entities
{
    public class Post : AuditableEntity
    {
        public string PostName {get; set;}
        public string PostImage {get; set;}
        public string Content {get; set;}
        public string PostTag {get; set;}
        public IList<Image> Images{get; set;} = new List<Image>();
    }
}
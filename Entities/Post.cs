using CMS_appBackend.Contracts;

namespace CMS_appBackend.Entities
{
    public class Post : AuditableEntity
    {
        public string PostName {get; set;}
        public string PostImage {get; set;}
        public string Content {get; set;}
        public string CreatedBy {get; set;}
        public string PostTag {get; set;}
        public DateTime CreatedOn {get; set;}
        public bool IsDeleted {get; set;}
    }
}
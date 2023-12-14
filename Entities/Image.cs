using CMS_appBackend.Contracts;
namespace CMS_appBackend.Entities
{
    public class Image : AuditableEntity
    {
        public string? Path{get; set;}
        public int blogId{get; set;}
        public Blog blog{get; set;}
        public int postId{get; set;}
        public Post post{get; set;}
        public int realEstateId{get; set;}
        public RealEstate realEstate{get; set;}
    }
}
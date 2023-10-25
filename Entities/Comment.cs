using CMS_appBackend.Contracts;
using CMS_appBackend.Entities.Identity;

namespace CMS_appBackend.Entities
{
    public class Comment : AuditableEntity
    {
        public string Detail { get; set; }
        public int BlogId { get; set; }
        public Blog? Blog { get; set; }
        public int? UserId    { get; set; }
        public User? User { get; set; }
    }
}

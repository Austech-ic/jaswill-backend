using CMS_appBackend.Contracts;
using CMS_appBackend.Identity;

namespace CMS_appBackend.Entities
{
    public class Comment : AuditableEntity
    {
        public string UserName { get; set; }
        public string CommentInput { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }

    }
}

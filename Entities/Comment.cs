using CMS_appBackend.Contracts;
using CMS_appBackend.Identity;

namespace CMS_appBackend.Entities
{
    public class Comment : AuditableEntity
    {
        public string CommentInput { get; set; }

    }
}

using CMS_appBackend.Contracts;
using CMS_appBackend.Identity;

namespace CMS_appBackend.Entities
{
    public class Admin : AuditableEntity
    {
        public bool IsSuperAdmin { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
        public bool IsApprove { get; set; }
    }
}

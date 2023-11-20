using CMS_appBackend.Contracts;
using CMS_appBackend.Entities;

namespace CMS_appBackend.Identity
{
    public class User : BaseUser
    {
        public Admin Admin { get; set; }
        public Customer Customer { get; set; }
        public ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
    }
}

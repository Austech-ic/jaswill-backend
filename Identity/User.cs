using CMS_appBackend.Contracts;
using CMS_appBackend.Entities;

namespace CMS_appBackend.Identity
{
    public class User : AuditableEntity
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Username { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? VerificationCode {get; set;}
        public Admin Admin { get; set; }
        public Customer Customer { get; set; }
        public ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
    }
}

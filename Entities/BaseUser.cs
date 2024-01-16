using CMS_appBackend.Contracts;

namespace CMS_appBackend.Entities
{
    public class BaseUser : AuditableEntity
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Username { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? VerificationCode { get; set; }
        public DateTime VerificationCodeExpiryTime { get; set; }
    }
}

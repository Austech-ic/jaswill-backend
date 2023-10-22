using System.ComponentModel.DataAnnotations;

namespace CMS_appBackend.DTOs.RequestModels
{
    public class CreateAdminRequestModel
    {
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password, ErrorMessage = "")]
        public string Password { get; set; }
        // public string Username { get; set; }
        // public string FirstName { get; set; }
        // public string LastName { get; set; }
        // public string PhoneNumber { get; set; }
    }
}

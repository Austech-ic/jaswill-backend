using System.ComponentModel.DataAnnotations;

namespace CMS_appBackend.DTOs.RequestModels
{
    public class LoginRequestModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

     public class ForgetPasswordRequestModel
    {
        public string Email { get; set; }
    }

    public class ResetPasswordRequestModel
    {
        public string Code { get; set; }

        [Required]
        [DataType(DataType.Password, ErrorMessage = "")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password, ErrorMessage = "")]
        public string ConfirmPassword { get; set; }
    }
}
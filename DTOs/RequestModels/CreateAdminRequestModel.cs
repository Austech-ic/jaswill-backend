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
        public string Username { get; set; }
        public string PhoneNumber { get; set; }
    }

    // public class CreateAdminRequestModelValidator : AbstractValidator<CreateAdminRequestModel>
    // {
    //     public CreateAdminRequestModelValidator()
    //     {
    //         RuleFor(x => x.Email).NotEmpty().EmailAddress();
    //         RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
    //     }
    // }

    public class ChangePasswordRequestModel
    {
        [Required]
        [DataType(DataType.Password, ErrorMessage = "")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password, ErrorMessage = "")]
        public string ConfirmPassword { get; set; }
    }
}

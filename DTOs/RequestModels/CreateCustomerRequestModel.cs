using System.ComponentModel.DataAnnotations;

namespace CMS_appBackend.DTOs.RequestModels
{
    public class CreateCustomerRequestModel
    {
        public string BankName { get; set; }

        [Required(ErrorMessage = "Account number is required.")]
        [StringLength(10, ErrorMessage = "Account number must be 10 digits.")]
        [RegularExpression(
            "^[0-9]{1,10}$",
            ErrorMessage = "Account number must be a numeric value with a maximum of 10 digits."
        )]
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string Address { get; set; }
        public string TypeOfPartner { get; set; }
        public DateTime DateOfReg { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class GetCustomerByTypeOfPartnerRequestModel
    {
        public string TypeOfPartner { get; set; }
    }
}

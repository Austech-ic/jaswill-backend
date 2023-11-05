namespace CMS_appBackend.DTOs
{
    public class CustomerDto
    {
        public int Id {get; set;}
        public string BankName {get; set;}
        public string AccountNumber {get; set;}
        public string AccountName {get; set;}
        public string Address {get; set;}
        public string TypeOfPartner {get; set;}
        public DateTime DateOfReg {get; set;}
        public string Username {get; set;}
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public string Email {get; set;}  
        public string PhoneNumber { get; set; }
    }
}
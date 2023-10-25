using CMS_appBackend.Contracts;
using CMS_appBackend.Entities.Identity;
namespace CMS_appBackend.Entities
{
    public class Customer : AuditableEntity
    {
        public string BankName {get; set;}
        public string AccountNumber {get; set;}
        public string AccountName {get; set;}
        public string Address {get; set;}
        public string TypeOfPartner {get; set;}
        public DateTime DateOfReg {get; set;}
        public int UserId {get; set;}
        public User User {get; set;}
    }
}
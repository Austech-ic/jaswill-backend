using System;

namespace CMS_appBackend.DTOs.RequestModels
{

    public class UpdateAdminRequestModels
    {
        public int Id { get; set; } 
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
       
    }
}
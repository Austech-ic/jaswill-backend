using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS_appBackend.DTOs
{
    public class AdminDto 
    {
        public int Id {get; set;}
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsSuperAdmin {get; set;}
        public bool IsApprove {get; set;}
    }
}
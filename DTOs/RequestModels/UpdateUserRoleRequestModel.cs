using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS_appBackend.DTOs.RequestModels
{
    public class UpdateUserRoleRequestModel
    {
        public int UserId {get; set;}
        public string RoleName {get; set;}
    }
}
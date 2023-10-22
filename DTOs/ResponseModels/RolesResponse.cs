using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS_appBackend.DTOs.ResponseModels
{
    public class RolesResponse : BaseResponse
    {
        public List<RoleDto> Data {get;set;} = new List<RoleDto>();
    } 

    public class RoleResponseModel : BaseResponse
    {
        public RoleDto Data {get;set;}
    }
}
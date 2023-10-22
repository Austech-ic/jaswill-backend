using CMS_appBackend.DTOs;

namespace CMS_appBackend.DTOs.ResponseModels
{
    public class UserResponseModel : BaseResponse 
    {
        public UserDto Data { get;set; }
    }
}
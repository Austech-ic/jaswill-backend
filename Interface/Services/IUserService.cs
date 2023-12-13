using CMS_appBackend.Identity;
using CMS_appBackend.DTOs.ResponseModels;
using CMS_appBackend.DTOs.RequestModels;
namespace CMS_appBackend.Interface.Services
{
    public interface IUserService
    {
        Task<UserResponseModel> Login(LoginRequestModel model);    
    }
}
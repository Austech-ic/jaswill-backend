using CMS_appBackend.Identity;
using CMS_appBackend.DTOs.ResponseModels;
namespace CMS_appBackend.Interface.Services
{
    public interface IUserService
    {
        Task<UserResponseModel> Login(loginRequestModel model);    
    }
}
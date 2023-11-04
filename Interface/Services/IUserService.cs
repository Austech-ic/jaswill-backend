using CMS_appBackend.Entities.Identity;
using CMS_appBackend.DTOs.ResponseModels;
namespace CMS_appBackend.Interface.Services
{
    public interface IUserService
    {
        Task<UserResponseModel> Login(string email, string password);    
    }
}
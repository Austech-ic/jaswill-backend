using CMS_appBackend.DTOs.ResponseModels;

namespace CMS_appBackend.Authentication
{
    public interface IJWTAuthentication
    {
        string GenerateToken(UserResponseModel model);
    }
}

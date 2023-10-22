using CMS_appBackend.DTOs.RequestModels;
using CMS_appBackend.DTOs.ResponseModels;

namespace CMS_appBackend.Interface.Repositories
{
    public interface IRoleService
    {
        Task<BaseResponse> AddRoleAsync(CreateRoleRequestmodel model);
        Task<RolesResponse> GetAllRoleAsync();
        Task<BaseResponse> UpdateUserRole(UpdateUserRoleRequestModel model);
        Task<RoleResponseModel> GetRoleByUserId(int id);
    }
}
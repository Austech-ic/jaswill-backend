using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS_appBackend.DTOs.RequestModels;
using CMS_appBackend.DTOs.ResponseModels;

namespace CMS_appBackend.Interface.Services
{
    public interface IAdminService
    {
        Task<BaseResponse> AddAdmin(CreateAdminRequestModel model);
        Task<BaseResponse> UpdateAdmin(UpdateAdminRequestModels model);
        Task<BaseResponse> ApproveAdmin(int Id);
        Task<BaseResponse> DeleteAdmin(int Id);
        Task<AdminsResponseModel> GetAllAdmin();
        Task<BaseResponse> AddUserRole(int UserId, int RoleId);
        Task<BaseResponse> ForgetPassword(ForgetPasswordRequestModel model);
        Task<BaseResponse> ResetPassword(ResetPasswordRequestModel model);
        Task<BaseResponse> ChangePassword(ChangePasswordRequestModel model, int id);
        Task<AdminResponseModel> GetAdminUserNameAndEmail(GetAdminByEmailAndUsernameRequestModel model);
        Task<AdminResponseModel> GetAdmin(int Id);
    }
}
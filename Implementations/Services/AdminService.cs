using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS_appBackend.DTOs;
using CMS_appBackend.Interface.Services;
using CMS_appBackend.DTOs.RequestModels;
using CMS_appBackend.DTOs.ResponseModels;
using CMS_appBackend.Entities;
using CMS_appBackend.Entities.Identity;
using CMS_appBackend.Interface.Repositories;

namespace CMS_appBackend.Implementations.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly IRoleRepository _roleRepository;

        public AdminService(
            IUserRepository userRepository,
            IAdminRepository adminRepository,
            IRoleRepository roleRepository
        )
        {
            _userRepository = userRepository;
            _adminRepository = adminRepository;
            _roleRepository = roleRepository;
        }

        public async Task<BaseResponse> AddAdmin(CreateAdminRequestModel model)
        {
            var admin = await _adminRepository.GetAsync(a => a.User.Email == model.Email);
            if (admin != null)
            {
                return new BaseResponse() { Message = "Admin Already Exist", Success = false, };
            }
            var newUser = new User { Email = model.Email, Password = model.Password, };
            var adduser = await _userRepository.CreateAsync(newUser);
            var role = await _roleRepository.GetAsync(x => x.Name.Equals("Admin"));
            if (role == null)
            {
                return new BaseResponse { Message = "Role not found", Success = false };
            }

            var userRole = new UserRole { UserId = adduser.Id, RoleId = role.Id, };
            adduser.UserRoles.Add(userRole);
            await _userRepository.UpdateAsync(adduser);

            var admins = new Admin { UserId = adduser.Id, User = adduser, };
            var addAdmin = await _adminRepository.CreateAsync(admins);
            return new BaseResponse { Message = "Admin Added Successfully", Success = true, };
        }

        public async Task<BaseResponse> DeleteAdmin(int Id)
        {
            var admin = await _adminRepository.GetAsync(
                admins => admins.IsDeleted == false && admins.Id == Id
            );
            if (admin == null)
            {
                return new BaseResponse { Message = "Admin not found", Success = false };
            }
            admin.IsDeleted = true;
            await _adminRepository.UpdateAsync(admin);
            return new BaseResponse
            {
                Message = "Administrator Successfully Deleted",
                Success = true
            };
        }

        public async Task<BaseResponse> ApproveAdmin(int Id)
        {
            var admin = await _adminRepository.GetAdminInfo(Id);
            if (admin == null)
            {
                return new BaseResponse { Message = "Admin not found", Success = false };
            }
            admin.IsApprove = true;
            await _adminRepository.UpdateAsync(admin);
            return new BaseResponse
            {
                Message = "Administrator Successfully Approved",
                Success = true
            };
        }

        public async Task<AdminsResponseModel> GetAllAdmin()
        {
            var admins = await _adminRepository.GetAdminsAsync();
            if (admins == null)
            {
                return new AdminsResponseModel
                {
                    Message = "No administrator found",
                    Success = false
                };
            }
            return new AdminsResponseModel
            {
                Success = true,
                Message = "Administrators Successfully Retrieved",
                Data = admins
                    .Select(
                        administrator =>
                            new AdminDto
                            {
                                Id = administrator.Id,
                                UserName = administrator.User.Username,
                                FirstName = administrator.User.FirstName,
                                LastName = administrator.User.LastName,
                                Email = administrator.User.Email,
                                PhoneNumber = administrator.User.PhoneNumber,
                                IsApprove = administrator.IsApprove,
                                IsSuperAdmin = administrator.IsSuperAdmin,
                            }
                    )
                    .ToList(),
            };
        }

        public async Task<BaseResponse> UpdateAdmin(UpdateAdminRequestModels model)
        {
            var admin = await _adminRepository.GetAdminInfo(model.Id);
            if (admin == null)
            {
                return new BaseResponse { Message = "Admin not found", Success = false };
            }
            admin.User.FirstName = model.FirstName;
            admin.User.LastName = model.LastName;
            admin.User.Email = model.Email;
            admin.User.PhoneNumber = model.PhoneNumber;
            await _adminRepository.UpdateAsync(admin);
            return new BaseResponse { Message = "Admin Updated Successfully", Success = true };
        }
    }
}

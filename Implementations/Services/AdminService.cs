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
using Microsoft.AspNetCore.Identity;
using CMS_appBackend.Email;

namespace CMS_appBackend.Implementations.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IEmailSender _email;

        public AdminService(
            IUserRepository userRepository,
            IAdminRepository adminRepository,
            IRoleRepository roleRepository,
            IEmailSender email,
            IPasswordHasher<User> passwordHasher
        )
        {
            _userRepository = userRepository;
            _adminRepository = adminRepository;
            _roleRepository = roleRepository;
            _passwordHasher = passwordHasher;
            _email = email;
        }

        public async Task<BaseResponse> AddAdmin(CreateAdminRequestModel model)
        {
            var admin = await _adminRepository.GetAsync(a => a.User.Email == model.Email);
            if (admin != null)
            {
                return new BaseResponse() { Message = "Admin Already Exist", Success = false, };
            }
            var newUser = new User
            {
                Email = model.Email,
                Password = _passwordHasher.HashPassword(null, model.Password),
                FirstName = model.FirstName,
                Username = model.Username,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
            };
            var adduser = await _userRepository.CreateAsync(newUser);

            var admins = new Admin { UserId = adduser.Id, User = adduser, };
            var addAdmin = await _adminRepository.CreateAsync(admins);
            return new BaseResponse { Message = "Admin Added Successfully", Success = true, };
        }

        public async Task<BaseResponse> AddUserRole(int UserId, int RoleId)
        {
            var user = await _userRepository.GetAsync(x => x.Id == UserId);
            if (user == null)
            {
                return new BaseResponse { Message = "User not found", Success = false };
            }
            var role = await _roleRepository.GetAsync(x => x.Id == RoleId);
            if (role == null)
            {
                return new BaseResponse { Message = "Role not found", Success = false };
            }
            var userRole = new UserRole { UserId = user.Id, RoleId = role.Id, };
            user.UserRoles.Add(userRole);
            await _userRepository.UpdateAsync(user);
            return new BaseResponse { Message = "User Role Added Successfully", Success = true };
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

            if (admins == null || !admins.Any())
            {
                return new AdminsResponseModel
                {
                    Message = "No administrators found",
                    Success = false
                };
            }

            var adminDtos = admins
                .Where(administrator => administrator.User != null) 
                .Select(
                    administrator =>
                        new AdminDto
                        {
                            Id = administrator.Id,
                            UserName = administrator.User!.Username, 
                            FirstName = administrator.User!.FirstName,
                            LastName = administrator.User!.LastName,
                            Email = administrator.User!.Email,
                            PhoneNumber = administrator.User!.PhoneNumber,
                            IsApprove = administrator.IsApprove,
                            IsSuperAdmin = administrator.IsSuperAdmin,
                        }
                )
                .ToList();

            return new AdminsResponseModel
            {
                Success = true,
                Message = "Administrators successfully retrieved",
                Data = adminDtos,
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

        public async Task<BaseResponse> ForgetPassword(ForgetPasswordRequestModel model, int Id)
        {
            var user = await _userRepository.GetAminById(Id);
            if (user == null)
            {
                return new BaseResponse { Message = "User not found", Success = false };
            }
            var code = Guid.NewGuid().ToString();
            user.VerificationCode = code;
             var mail = new EmailRequestModel
            {
                ReceiverEmail = model.Email,
                ReceiverName = model.Email,
                Message = $"Verification Code : {code}\nand enter The verification code attached to this Mail to complete your registratio.",
                Subject = "Jaswill-Real Estate Email Verification",
            };
            await _email.SendEmail(mail);
            await _userRepository.UpdateAsync(user);
            return new BaseResponse { Message = "Verification Code Sent Successfully", Success = true };
        }

        public async Task<BaseResponse> ResetPassword(ResetPasswordRequestModel model, String code)
        {
            var user = await _userRepository.GetAsync(x => x.VerificationCode == code);
            if (user == null)
            {
                return new BaseResponse { Message = "User not found", Success = false };
            }
            user.Password = _passwordHasher.HashPassword(null, model.NewPassword);
            user.Password = _passwordHasher.HashPassword(null, model.ConfirmPassword);
            await _userRepository.UpdateAsync(user);
            return new BaseResponse { Message = "Password Reset Successfully", Success = true };
        }

        public async Task<BaseResponse> ChangePassword(ChangePasswordRequestModel model, int id)
        {
            var user = await _userRepository.GetAsync(x => x.Id == id);
            if (user == null)
            {
                return new BaseResponse { Message = "User not found", Success = false };
            }
            user.Password = _passwordHasher.HashPassword(null, model.NewPassword);
            user.Password = _passwordHasher.HashPassword(null, model.ConfirmPassword);
            await _userRepository.UpdateAsync(user);
            return new BaseResponse { Message = "Password Changed Successfully", Success = true };
        }
    }
}

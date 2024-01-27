using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS_appBackend.DTOs;
using CMS_appBackend.Interface.Services;
using CMS_appBackend.DTOs.RequestModels;
using CMS_appBackend.DTOs.ResponseModels;
using CMS_appBackend.Entities;
using CMS_appBackend.Identity;
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
            var admin = await _adminRepository.GetAsync(a => a.User.Email == model.Email && a.IsDeleted == false);
            if (admin != null)
            {
                return new BaseResponse() { Message = "Admin Already Exist", Success = false, };
            }
            var newUser = new User
            {
                Email = model.Email,
                Password = _passwordHasher.HashPassword(null, model.Password),
                Username = model.Username,
                PhoneNumber = model.PhoneNumber,
                IsDeleted = false,
            };
            var adduser = await _userRepository.CreateAsync(newUser);

            var admins = new Admin
            {
                UserId = adduser.Id,
                User = adduser,
                IsApprove = true,
            };
            var addAdmin = await _adminRepository.CreateAsync(admins);
            return new BaseResponse { Message = "Admin Added Successfully", Success = true, };
        }

        public async Task<BaseResponse> AddUserRole(int userId, int roleId)
        {
            // Check if the user exists
            var user = await _userRepository.GetAsync(x => x.Id == userId);
            if (user == null)
            {
                return new BaseResponse { Message = "User not found", Success = false };
            }

            // Check if the role exists
            var role = await _roleRepository.GetAsync(x => x.Id == roleId);
            if (role == null)
            {
                return new BaseResponse { Message = "Role not found", Success = false };
            }

            // Check if the user already has the role
            var check = _adminRepository.VerifyAdminRole(roleId);
            if (check != null)
            {
                return new BaseResponse
                {
                    Message = "User already has the specified role",
                    Success = false
                };
            }

            // If not, add the role to the user
            var userRole = new UserRole { UserId = user.Id, RoleId = role.Id };
            user.UserRoles.Add(userRole);

            // Update the user in the repository
            await _userRepository.UpdateAsync(user);

            return new BaseResponse { Message = "User Role Added Successfully", Success = true };
        }

        public async Task<BaseResponse> DeleteAdmin(int Id)
        {
            var admin = await _adminRepository.GetAdminInfo(Id);
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
            admin.User.FirstName = model.FirstName ?? admin.User.FirstName;
            admin.User.LastName = model.LastName ?? admin.User.LastName;
            admin.User.Email = model.Email ?? admin.User.Email;
            admin.User.PhoneNumber = model.PhoneNumber ?? admin.User.PhoneNumber;
            await _adminRepository.UpdateAsync(admin);
            return new BaseResponse { Message = "Admin Updated Successfully", Success = true };
        }

        public async Task<BaseResponse> ForgetPassword(ForgetPasswordRequestModel model)
        {
            var user = await _userRepository.GetAsync(x => x.Email == model.Email);
            if (user == null)
            {
                return new BaseResponse { Message = "User not found", Success = false };
            }
            var code = new Random().Next(1000, 10000);
            var expiryTime = DateTime.UtcNow.AddMinutes(15);
            user.VerificationCode = code.ToString();
            user.VerificationCodeExpiryTime = expiryTime;
            var mail = new EmailRequestModel
            {
                ReceiverEmail = model.Email,
                ReceiverName = model.Email,
                Message =
                    $"Verification Code : {code}\nand enter The verification code attached to this Mail to complete your registratio. And note that the code will expire in 15 minutes",
                Subject = "Jaswill-Real Estate Email Verification",
            };
            await _email.SendEmail(mail);
            await _userRepository.UpdateAsync(user);
            return new BaseResponse
            {
                Message = "Verification Code Sent Successfully",
                Success = true
            };
        }

        public async Task<BaseResponse> ResetPassword(ResetPasswordRequestModel model)
        {
            var user = await _userRepository.GetAsync(x => x.VerificationCode == model.Code);
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

        public async Task<AdminResponseModel> GetAdminUserNameAndEmail(GetAdminByEmailAndUsernameRequestModel model)
        {
            var admin = await _adminRepository.GetAdminByEmailAndUsername(model.Email, model.Username);
            if (admin == null)
            {
                return new AdminResponseModel { Message = "Admin not found", Success = false };
            }
            var adminDto = new AdminDto
            {
                UserName = admin.User!.Username,
                Email = admin.User!.Email,
            };
            return new AdminResponseModel
            {
                Message = "Admin successfully retrieved",
                Success = true,
                Data = adminDto,
            };
        }

        public async Task<AdminResponseModel> GetAdmin(int Id)
        {
            var admin = await _adminRepository.GetAdminInfo(Id);
            if (admin == null)
            {
                return new AdminResponseModel { Message = "Admin not found", Success = false };
            }
            var adminDto = new AdminDto
            {
                Id = admin.Id,
                UserName = admin.User!.Username,
                FirstName = admin.User!.FirstName,
                LastName = admin.User!.LastName,
                Email = admin.User!.Email,
                PhoneNumber = admin.User!.PhoneNumber,
                IsApprove = admin.IsApprove,
                IsSuperAdmin = admin.IsSuperAdmin,
            };
            return new AdminResponseModel
            {
                Message = "Admin successfully retrieved",
                Success = true,
                Data = adminDto,
            };
        }
    }
}

using CMS_appBackend.DTOs;
using CMS_appBackend.DTOs.RequestModels;
using CMS_appBackend.DTOs.ResponseModels;
using CMS_appBackend.Identity;
using CMS_appBackend.Interface.Repositories;

namespace CMS_appBackend.Implementations.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;

        public RoleService(IRoleRepository roleRepository, IUserRepository userRepository)
        {
            _roleRepository = roleRepository;
            _userRepository = userRepository;
        }


        public async Task<BaseResponse> AddRoleAsync(CreateRoleRequestmodel model)
        {
            var role = await _roleRepository.GetAsync(r => r.Name == model.Name && r.IsDeleted == false);
            if (role != null)
            {
                return new BaseResponse()
                {
                    Message = "Role Already Exist",
                    Success = false,
                };
            }
            var newRole = new Role
            {
                Name = model.Name,
                Description = model.Description,
            };
            await _roleRepository.CreateAsync(newRole);
            return new BaseResponse
            {
                Message = "Role Created Successfully",
                Success = true,
            };
        }

        public async Task<RolesResponse> GetAllRoleAsync()
        {
            var role = await _roleRepository.GetAllAsync();
            if (role == null)
            {
                 return new RolesResponse
                {
                    Message = "No Roles Found",
                    Success = false,
                };
            }
            return new RolesResponse
            {
                Data = role.Select(x => new RoleDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                }).ToList(),
                Message = "Roles Found Successfully",
                Success = true
            };
        }
        public async Task<RoleResponseModel> GetRoleByUserId(int id)
        {
            var role = await _roleRepository.GetRoleByUserId(id);
            if (role == null)
            {
                return new RoleResponseModel{
                    Message = "Role not found",
                    Success = false,
                };
            }
            return new RoleResponseModel
            {
                Message = "Role found",
                Success = true,
                Data = new RoleDto
                {
                    Id = role.Id,
                    Name = role.Name,
                    Description = role.Description
                }
            };
        }

        public async Task<BaseResponse> UpdateUserRole(UpdateUserRoleRequestModel model)
        {
            var user = await _userRepository.GetAsync(u => u.Id == model.UserId);
            if (user == null)
            {
                return new BaseResponse
                {
                    Message = "User Not Found",
                    Success = false,
                };
            }
            var updateUserRole = user.UserRoles.Where(x => x.UserId == user.Id).ToList();
            var getRole = await _roleRepository.GetAsync(x => x.Name == model.RoleName);
            foreach (var item in updateUserRole)
            {
                item.RoleId = getRole.Id;
            }
            user.UserRoles = updateUserRole;
            await _userRepository.UpdateAsync(user);
            return new BaseResponse
            {
                Message = "User Role Updated Successfully",
                Success = true,
            };
        }
    }
}

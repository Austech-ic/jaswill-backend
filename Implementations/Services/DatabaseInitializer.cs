using CMS_appBackend.Entities.Identity;
using CMS_appBackend.Interface.Repositories;
using Microsoft.AspNetCore.Identity;
using CMS_appBackend.Entities;
using System.Security.Cryptography;
using System.Text;

namespace CMS_appBackend.Implementations.Services
{
    public class DatabaseInitializer
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        public DatabaseInitializer(
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            IPasswordHasher<User> passwordHasher,
            IAdminRepository adminRepository
        )
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _passwordHasher = passwordHasher;
            _adminRepository = adminRepository;
        }

        public async Task Initialize()
        {
            var admin = await _userRepository.GetAsync(u => u.Email == "admin@jaswill.com");
            if (admin == null)
            {
                var newAdmin = new User
                {
                    Id = 1,
                    Email = "admin@jaswill.com",
                    FirstName = "Mr",
                    LastName = "Admin",
                    Username = "Admin",
                    PhoneNumber = "+234",
                };

                newAdmin.Password = _passwordHasher.HashPassword(newAdmin, "123456789");

                var role = await _roleRepository.GetAsync(r => r.Name == "SuperAdmin");
                if (role == null)
                {
                    role = new Role { Name = "SuperAdmin", Description = "The Owner", };
                    await _roleRepository.CreateAsync(role);
                }
                var userRole = new UserRole { UserId = newAdmin.Id, RoleId = role.Id };
                newAdmin.UserRoles.Add(userRole);
                var adduser = await _userRepository.CreateAsync(newAdmin);
                var admins = new Admin { UserId = adduser.Id, User = adduser, IsSuperAdmin = true,};
                var addAdmin = await _adminRepository.CreateAsync(admins);
            }
        }
    }
}

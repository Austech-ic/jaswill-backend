using CMS_appBackend.DTOs;
using CMS_appBackend.DTOs.ResponseModels;
using CMS_appBackend.Interface.Repositories;
using CMS_appBackend.DTOs.RequestModels;
using CMS_appBackend.Interface.Services;
using Microsoft.AspNetCore.Identity;
using CMS_appBackend.Identity;

namespace CMS_appBackend.Implementations.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(IUserRepository repository, IPasswordHasher<User> passwordHasher)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
        }

        public async Task<UserResponseModel> Login(LoginRequestModel model)
        {
            var user = await _repository.GetAdminEmailandPassword(model.Email);
            if (
                user == null
                || _passwordHasher.VerifyHashedPassword(null, user.Password, model.Password)
                    != PasswordVerificationResult.Success
            )
            {
                return new UserResponseModel
                {
                    Success = false,
                    Message = "Email or password is incorrect"
                };
            }

            // User found and password matches, create a response
            var userDto = new UserDto
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.Id,
                PhoneNumber = user.PhoneNumber,
                Username = user.Username,
                VerificationCode = user.VerificationCode,
            };

            return new UserResponseModel
            {
                Success = true,
                Message = "Login successfully",
                Data = userDto
            };
        }

        public bool VerifyPassword(string enteredPassword, string hashedPassword)
        {
            // Compare the hashed entered password with the stored hashed password
            return _passwordHasher.VerifyHashedPassword(null, hashedPassword, enteredPassword)
                == PasswordVerificationResult.Success;
        }
    }
}

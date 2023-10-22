using CMS_appBackend.DTOs;
using CMS_appBackend.DTOs.ResponseModels;
using CMS_appBackend.Interface.Repositories;
using CMS_appBackend.Interface.Services;

namespace CMS_appBackend.Implementations.Services
{
    public class UserService : IUserService

    {
        private readonly IUserRepository _repository;
        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserResponseModel> Login(string email, string password)
        {

            var user = await _repository.GetAsync(x => x.Email.Equals(email) && x.Password.Equals(password));
            if (user != null)
            {
                return new UserResponseModel
                {
                    Data = new UserDto(){
                        Email = user.Email,
                        Password = user.Password,
                        Id = user.Id
                    },
                    Success = true,
                    Message = "Sucessfully logged in",
                };
            }
            return new UserResponseModel
            {
                Success = false,
                Message = "Loggin Failed",
            };
        }
    }
}
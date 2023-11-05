using CMS_appBackend.Context;
using CMS_appBackend.Entities;
using CMS_appBackend.Identity;
using CMS_appBackend.Interface.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CMS_appBackend.Implementations.Repositories
{
    public class UserRepository: GenericRepository<User>, IUserRepository 
    {
        public UserRepository(ApplicationContext Context)
        {
            _Context = Context;
        }
        
        public async Task<User> GetAdminEmailandPassword(string email)
        {
            var user = await _Context.Users.FirstOrDefaultAsync(x => x.Email == email);
            return user;
        }

        public async Task<User> GetAminById(int id)
        {
            var user = await _Context.Users.SingleOrDefaultAsync(x => x.Id == id);
            return user;
        }
    }
}
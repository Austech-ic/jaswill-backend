using CMS_appBackend.Context;
using CMS_appBackend.Entities;
using CMS_appBackend.Entities.Identity;
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
        
    }
}
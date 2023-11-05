using CMS_appBackend.Entities;
using CMS_appBackend.Identity;

namespace CMS_appBackend.Interface.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetAdminEmailandPassword(string email);
        Task<User> GetAminById(int id);
    }
}
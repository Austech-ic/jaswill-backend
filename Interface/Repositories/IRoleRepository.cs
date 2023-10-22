using CMS_appBackend.DTOs.RequestModels;
using CMS_appBackend.DTOs.ResponseModels;
using CMS_appBackend.Entities;
using CMS_appBackend.Entities.Identity;
using System.Linq.Expressions;
using System.Threading.Tasks;
namespace CMS_appBackend.Interface.Repositories
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        Task<Role> GetRoleByUserId(int id);
    }
}
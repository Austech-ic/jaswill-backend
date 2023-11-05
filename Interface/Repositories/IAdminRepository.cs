using CMS_appBackend.Entities;
using CMS_appBackend.Identity;
using System.Linq.Expressions;
using System.Threading.Tasks;
namespace CMS_appBackend.Interface.Repositories
{
    public interface IAdminRepository : IGenericRepository<Admin>
    {
        Task<Admin> GetAdminInfo(int id);
        Task<IList<Admin>> GetAllAdmins();
        Task<IList<Admin>> GetAllDeletedAdmin();
        Task<IList<Admin>> GetAllSuperAdmin();
        Task<IList<Admin>> GetNonSuperAdmins();
        Task<List<Admin>> GetAdminsAsync();
        Task<Admin> GetVerificationCode(string code);
        Task<Admin> VerifyAdminRole(int roleId);
    }
}

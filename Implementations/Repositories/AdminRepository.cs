using CMS_appBackend.Context;
using CMS_appBackend.Entities;
using CMS_appBackend.Identity;
using CMS_appBackend.Interface.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CMS_appBackend.Implementations.Repositories
{
    public class AdminRepository : GenericRepository<Admin>, IAdminRepository
    {
        public AdminRepository(ApplicationContext Context)
        {
            _Context = Context;
        }

        public async Task<Admin> GetAdminInfo(int id)
        {
            var admin = await _Context.Admins
                .Include(x => x.User)
                .SingleOrDefaultAsync(x => x.Id == id);
            return admin;
        }

        public async Task<IList<Admin>> GetAllAdmins()
        {
            var admins = await _Context.Admins
                .Include(x => x.User)
                .Where(x => x.IsDeleted == false)
                .ToListAsync();
            return admins;
        }

        public async Task<IList<Admin>> GetAllDeletedAdmin()
        {
            var admins = await _Context.Admins
                .Include(x => x.User)
                .Where(x => x.IsDeleted == true)
                .ToListAsync();
            return admins;
        }

        public async Task<IList<Admin>> GetAllSuperAdmin()
        {
            var admins = await _Context.Admins
                .Include(x => x.User)
                .Where(x => x.IsSuperAdmin == true && x.IsDeleted == false)
                .ToListAsync();
            return admins;
        }

        public async Task<IList<Admin>> GetNonSuperAdmins()
        {
            var admins = await _Context.Admins
                .Include(x => x.User)
                .Where(x => x.IsSuperAdmin == false && x.IsDeleted == false)
                .ToListAsync();
            return admins;
        }

        public async Task<List<Admin>> GetAdminsAsync()
        {
            var admins = await _Context.Admins
                .Include(a => a.User)
                .Where(adm => adm.IsDeleted == false)
                .ToListAsync();
            return admins;
        }

        public async Task<Admin> ApproveAdmin(bool isApproved, int id)
        {
            var admin = await _Context.Admins.SingleOrDefaultAsync(x => x.Id == id);
            admin.IsApprove = isApproved;
            await _Context.SaveChangesAsync();
            return admin;
        }

        public async Task<Admin> GetVerificationCode(string code)
        {
            var admin = await _Context.Admins
                .Include(x => x.User)
                .SingleOrDefaultAsync(x => x.User.VerificationCode == code);
            return admin;
        }

        public async Task<Admin> VerifyAdminRole(int roleId)
        {
            var admin = await _Context.Admins
                .Include(x => x.User)
                .ThenInclude(x => x.UserRoles)
                .SingleOrDefaultAsync(x => x.User.UserRoles.Any(ur => ur.RoleId == roleId));

            return admin;
        }
    }
}

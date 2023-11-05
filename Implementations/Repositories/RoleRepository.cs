
using CMS_appBackend.Context;
using CMS_appBackend.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CMS_appBackend.Interface.Repositories;
using CMS_appBackend.Entities;
using CMS_appBackend.Entities;
using Microsoft.EntityFrameworkCore;
namespace CMS_appBackend.Implementations.Repositories
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(ApplicationContext Context)
        {
            _Context = Context;
        }
         public async Task<Role> GetRoleByUserId(int id)
        {
            var role = await _Context.UserRoles.Include(c => c.User).Include(x => x.Role).SingleOrDefaultAsync(x => x.Id == id);
            return role.Role;
        }

     
    }
}
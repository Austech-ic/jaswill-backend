
using CMS_appBackend.Context;
using CMS_appBackend.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CMS_appBackend.Interface.Repositories;
using CMS_appBackend.Entities;
using Microsoft.EntityFrameworkCore;
namespace CMS_appBackend.Implementations.Repositories
{
    public class RealEstateRepository : GenericRepository<RealEstate> , IRealEstateRepository
    {
        public RealEstateRepository(ApplicationContext Context)
        {
            _Context = Context;
        }
        
        public async Task<IList<RealEstate>> GetAllRealEstate()
        {
            var realEstate = await _Context.RealEstates.Include(x => x.Images).ToListAsync();
            return realEstate;
        }

        public async Task<RealEstate> GetRealEstateById(int id)
        {
            var realEstate = await _Context.RealEstates.Include(x => x.Images).SingleOrDefaultAsync(x => x.Id == id);
            return realEstate;
        }
    }
}
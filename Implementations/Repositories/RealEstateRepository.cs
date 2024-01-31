
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
        
        public async Task<IList<RealEstate>> GetAllRealEstatesAsync()
        {
            var realEstates = await _Context.RealEstates
                .Include(x => x.Images)
                .Include(x => x.Category)
                .Where(x => x.IsDeleted == false)
                .ToListAsync();
            return realEstates;
        }

        public async Task<RealEstate> GetRealEstateById(int id)
        {
            var realEstate = await _Context.RealEstates.Include(x => x.Category).Include(x => x.Images).
            SingleOrDefaultAsync(x => x.Id == id);
            return realEstate;
        }

        public async Task<IList<RealEstate>> GetRealEstatesByCategoryId(int id)
        {
            var realEstates = await _Context.RealEstates.Include(x => x.Category).Include(x => x.Images).Where(x => x.Id == id).ToListAsync();
            return realEstates;
        }

        public async Task<IList<RealEstate>> GetRealEstatesByType(string type)
        {
            var realEstates = await _Context.RealEstates.Include(x => x.Category).Include(x => x.Images).Where(x => x.Type == type).ToListAsync();
            return realEstates;
        }

        public async Task<IList<RealEstate>> GetRealEstateByCategoryName(string categoryName)
        {
            var realEstates = await _Context.RealEstates.Include(x => x.Category).Include(x => x.Images).Where(x => x.CategoryName == categoryName).ToListAsync();
            return realEstates;
        }

        public async Task<IList<RealEstate>> GetRealEstateByCategoryId (int id)
        {
            var realEstates = await _Context.RealEstates.Include(x => x.Category).Include(x => x.Images).Where(x => x.CategoryId == id).ToListAsync();
            return realEstates;
        }

        public async Task<IList<RealEstate>> GetAllRealEstatesByCategories()
        {
            var realEstates = await _Context.RealEstates.Include(x => x.Category).Include(x => x.Images).ToListAsync();
            return realEstates;
        }
    }
}
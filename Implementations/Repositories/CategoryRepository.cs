using Microsoft.EntityFrameworkCore;
using CMS_appBackend.Context;
using CMS_appBackend.Entities;
using CMS_appBackend.Interface.Repositories;

namespace CMS_appBackend.Implementations.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationContext context)
        {
            _Context = context;
        }

        public async Task<IList<Category>> GetAll()
        {
            var categories = await _Context.Categories.Where(x => x.IsDeleted == false).Include(x => x.RealEstates).ToListAsync();
            return categories;
        }

        public async Task<Category> GetCategory(int id)
        {
            var category = await _Context.Categories.Include(x => x.RealEstates).SingleOrDefaultAsync(x => x.Id == id);
            return category;
        }

        public async Task<Category> GetById(int id)
        {
            var category = _Context.Categories.Include(x => x.RealEstates).SingleOrDefault(x => x.Id == id);
            return category;
        }

        public async Task<IList<Category>> GetAllWithInfo()
        {
            var categories = await _Context.Categories.Include(x => x.RealEstates).ToListAsync();
            return categories;
        }

        public async Task<IList<Category>> GetCategoriesToDisplay()
        {
            var categories = await _Context.Categories.Include(x => x.RealEstates).Where(x => x.IsDeleted == false).ToListAsync();
            return categories;
        }

        public async Task<Category> GetCategoryByName(string name)
        {
            var category = await _Context.Categories.Include(x => x.RealEstates).SingleOrDefaultAsync(x => x.CategoryName == name);
            return category;
        }
    }
}

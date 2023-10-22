using CMS_appBackend.Entities;
using CMS_appBackend.DTOs.ResponseModels;
using CMS_appBackend.DTOs.RequestModel;

namespace CMS_appBackend.Interface.Repositories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<Category> GetCategory(int id);
        Task<Category> GetById(int id);
        Task<IList<Category>> GetAll();
        Task<IList<Category>> GetAllWithInfo();
    }
}

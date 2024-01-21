using CMS_appBackend.Entities;

namespace CMS_appBackend.Interface.Repositories
{
    public interface IRealEstateRepository : IGenericRepository<RealEstate>
    {
        Task<IList<RealEstate>> GetAllRealEstatesAsync();
        Task<RealEstate> GetRealEstateById(int id);
        Task<IList<RealEstate>> GetRealEstatesByCategoryId(int id);
        Task<IList<RealEstate>> GetRealEstatesByType(string type);
    }
}
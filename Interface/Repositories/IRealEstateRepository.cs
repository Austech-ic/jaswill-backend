using CMS_appBackend.Entities;

namespace CMS_appBackend.Interface.Repositories
{
    public interface IRealEstateRepository : IGenericRepository<RealEstate>
    {
        Task<IList<RealEstate>> GetAllRealEstate();
        Task<RealEstate> GetRealEstateById(int id);
    }
}
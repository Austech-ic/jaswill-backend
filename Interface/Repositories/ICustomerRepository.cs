using CMS_appBackend.Entities;

namespace CMS_appBackend.Interface.Repositories
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Task<Customer> GetCustomer(int id);
        Task<IList<Customer>> GetAll();
        Task<Customer> GetCutomerByTypeOfPartner(string typeOfPartner);
        Task<Customer> GetVerificationCode(string code);
    }
}
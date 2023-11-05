using CMS_appBackend.Entities;
using CMS_appBackend.Identity;
using CMS_appBackend.Interface.Repositories;
using CMS_appBackend.Context;
using Microsoft.EntityFrameworkCore;

namespace CMS_appBackend.Implementations.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationContext context)
        {
            _Context = context;
        }

        public async Task<IList<Customer>> GetAll()
        {
            var customers = await _Context.Customers
                .Where(x => x.IsDeleted == false)
                .Include(x => x.User)
                .OrderByDescending(x => x.CreatedOn)
                .ToListAsync();
            return customers;
        }

        public async Task<Customer> GetCustomer(int id)
        {
            // Ensure that both conditions are considered in the repository method
            return await _Context.Customers
                .Where(cms => cms.User.Id == id && cms.IsDeleted == false)
                .Include(cus => cus.User)
                .FirstOrDefaultAsync();
        }

        public async Task<Customer> GetCutomerByTypeOfPartner(string typeOfPartner)
        {
            var customer = await _Context.Customers
                .Where(x => x.IsDeleted == false && x.TypeOfPartner == typeOfPartner)
                .Include(x => x.User)
                .FirstOrDefaultAsync();
            return customer;
        }

        public async Task<Customer> GetVerificationCode(string code)
        {
            var customer = await _Context.Customers
                .Where(x => x.IsDeleted == false && x.User.VerificationCode == code)
                .Include(x => x.User)
                .FirstOrDefaultAsync();
            return customer;
        }

        public async Task<Customer> GetCustomerByEmail(string email)
        {
            var customer = await _Context.Customers
                .Include(x => x.User)
                .Where(x => x.IsDeleted == false && x.User.Email == email)
                .FirstOrDefaultAsync();
            return customer;
        }
    }
}

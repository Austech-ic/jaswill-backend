using CMS_appBackend.Interface.Repositories;
using CMS_appBackend.DTOs.ResponseModels;
using CMS_appBackend.Entities;
using CMS_appBackend.Entities.Identity;
using CMS_appBackend.DTOs.RequestModels;
using CMS_appBackend.DTOs;
using CMS_appBackend.Interface.Services;

namespace CMS_appBackend.Implementations.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUserRepository _userRepository;

        public CustomerService(
            ICustomerRepository customerRepository,
            IUserRepository userRepository
        )
        {
            _customerRepository = customerRepository;
            _userRepository = userRepository;
        }

        public async Task<BaseResponse> CreateCustomer(CreateCustomerRequestModel model)
        {
            var user = await _customerRepository.GetAsync(x => x.User.Email.Equals(model.Email));
            if (user == null)
            {
                return new BaseResponse { Message = "User not found", Success = false, };
            }
            var newCustomer = new User
            {
                Email = model.Email,
                Password = model.Password,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Username = model.Username,
            };
            var adduser = await _userRepository.CreateAsync(newCustomer);
            var customer = new Customer
            {
                UserId = adduser.Id,
                User = adduser,
                Address = model.Address,
                BankName = model.BankName,
                AccountNumber = model.AccountNumber,
                AccountName = model.AccountName,
                TypeOfPartner = model.TypeOfPartner,
            };
            await _customerRepository.CreateAsync(customer);
            return new BaseResponse { Message = "Create customer successfully", Success = true, };
        }

        public async Task<CustomersResponseModel> GetAll()
        {
            var customers = await _customerRepository.GetAllAsync();
            if (customers == null & !customers.Any())
            {
                return new CustomersResponseModel
                {
                    Message = "Customers not found",
                    Success = false,
                };
            }
            var customerResponse = new CustomersResponseModel
            {
                Message = "Get all customers successfully",
                Success = true,
                Data = customers
                    .Where(c => c.User != null)
                    .Select(
                        x =>
                            new CustomerDto
                            {
                                Id = x.Id,
                                Address = x.Address,
                                BankName = x.BankName,
                                AccountNumber = x.AccountNumber,
                                AccountName = x.AccountName,
                                TypeOfPartner = x.TypeOfPartner,
                                FirstName = x.User!.FirstName,
                                LastName = x.User!.LastName,
                                Email = x.User!.Email,
                                PhoneNumber = x.User!.PhoneNumber,
                                Username = x.User!.Username,
                            }
                    )
                    .ToList(),
            };
            return customerResponse;
        }

        public async Task<CustomerResponseModel> GetCustomer(int id)
        {
            var customer = await _customerRepository.GetAsync(x => x.Id == id);
            if (customer == null)
            {
                return new CustomerResponseModel
                {
                    Message = "Customer not found",
                    Success = false,
                };
            }
            var customerResponse = new CustomerResponseModel
            {
                Message = "Get customer successfully",
                Success = true,
                Data = new CustomerDto
                {
                    Id = customer.Id,
                    Address = customer.Address,
                    BankName = customer.BankName,
                    AccountNumber = customer.AccountNumber,
                    AccountName = customer.AccountName,
                    TypeOfPartner = customer.TypeOfPartner,
                    FirstName = customer.User.FirstName,
                    LastName = customer.User.LastName,
                    Email = customer.User.Email,
                    PhoneNumber = customer.User.PhoneNumber,
                    Username = customer.User.Username,
                },
            };
            return customerResponse;
        }

        public async Task<CustomerResponseModel> GetCustomerByTypeOfPartner(string typeOfPartner)
        {
            var customer = await _customerRepository.GetAsync(
                x => x.TypeOfPartner.Equals(typeOfPartner)
            );
            if (customer == null)
            {
                return new CustomerResponseModel
                {
                    Message = "Customer not found",
                    Success = false,
                };
            }
            var customerResponse = new CustomerResponseModel
            {
                Message = "Get customer successfully",
                Success = true,
                Data = new CustomerDto
                {
                    Id = customer.Id,
                    Address = customer.Address,
                    BankName = customer.BankName,
                    AccountNumber = customer.AccountNumber,
                    AccountName = customer.AccountName,
                    TypeOfPartner = customer.TypeOfPartner,
                    FirstName = customer.User.FirstName,
                    LastName = customer.User.LastName,
                    Email = customer.User.Email,
                    PhoneNumber = customer.User.PhoneNumber,
                    Username = customer.User.Username,
                },
            };
            return customerResponse;
        }
    }
}

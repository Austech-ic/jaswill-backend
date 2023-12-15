using CMS_appBackend.Interface.Repositories;
using CMS_appBackend.DTOs.ResponseModels;
using CMS_appBackend.Entities;
using CMS_appBackend.Identity;
using CMS_appBackend.DTOs.RequestModels;
using CMS_appBackend.DTOs;
using CMS_appBackend.Interface.Services;
using Microsoft.AspNetCore.Identity;
using CMS_appBackend.Email;

namespace CMS_appBackend.Implementations.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IEmailSender _email;

        public CustomerService(
            ICustomerRepository customerRepository,
            IUserRepository userRepository,
            IEmailSender email,
            IPasswordHasher<User> passwordHasher
        )
        {
            _customerRepository = customerRepository;
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _email = email;
        }

        public async Task<BaseResponse> CreateCustomer(CreateCustomerRequestModel model)
        {
            var admin = await _customerRepository.GetAsync(a => a.User.Email == model.Email);
            if (admin != null)
            {
                return new BaseResponse { Message = "User Already Exist", Success = false, };
            }
            var newCustomer = new User
            {
                Email = model.Email,
                Password = _passwordHasher.HashPassword(null, model.Password),
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Username = model.Username,
            };
            newCustomer.Password = _passwordHasher.HashPassword(newCustomer, model.Password);
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
                                FirstName = x.FirstName,
                                LastName = x.LastName,
                                Email = x.Email,
                                PhoneNumber = x.PhoneNumber,
                                Username = x.Username,
                            }
                    )
                    .ToList(),
            };
            return customerResponse;
        }

        public async Task<CustomerResponseModel> GetCustomer(int id)
        {
            var customer = await _customerRepository.GetCustomer(id);

            if (customer == null)
            {
                return new CustomerResponseModel
                {
                    Message = "Customer not found",
                    Success = false,
                };
            }
            return new CustomerResponseModel
            {
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
                    DateOfReg = customer.DateOfReg,
                },
                Message = "Get customer successfully",
                Success = true,
            };
        }

        public async Task<CustomerResponseModel> GetCustomerByTypeOfPartner(GetCustomerByTypeOfPartnerRequestModel model)
        {
            var customer = await _customerRepository.GetCutomerByTypeOfPartner(model.TypeOfPartner);
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
                    DateOfReg = customer.DateOfReg,
                },
            };
            return customerResponse;
        }

        public async Task<BaseResponse> ForgetPassword(
            ForgetPasswordRequestModel model
        )
        {
            var user = await _userRepository.GetAsync(x => x.Email.Equals(model.Email));
            if (user == null)
            {
                return new BaseResponse { Message = "User not found", Success = false, };
            }
            var code = Guid.NewGuid().ToString();
            user.VerificationCode = code;
            var mail = new EmailRequestModel
            {
                ReceiverEmail = model.Email,
                ReceiverName = model.Email,
                Message =
                    $"Verification Code : {code}\nand enter The verification code attached to this Mail to complete your registratio.",
                Subject = "Relief-CMS Email Verification",
            };
            await _email.SendEmail(mail);
            await _userRepository.UpdateAsync(user);
            return new BaseResponse { Message = "Forget password successfully", Success = true, };
        }

        public async Task<BaseResponse> ResetPassword(ResetPasswordRequestModel model)
        {
            var user = await _userRepository.GetAsync(x => x.VerificationCode.Equals(model.Code));
            if (user == null)
            {
                return new BaseResponse { Message = "User not found", Success = false, };
            }
            user.Password = model.NewPassword;
            user.Password = model.ConfirmPassword;
            await _userRepository.UpdateAsync(user);
            return new BaseResponse { Message = "Reset password successfully", Success = true, };
        }
    }
}

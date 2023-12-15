using CMS_appBackend.DTOs.RequestModels;
using CMS_appBackend.Interface.Repositories;
using CMS_appBackend.Interface.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
namespace CMS_appBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IUserService _userService;

        public CustomerController(ICustomerService customerService, IUserService userService)
        {
            _customerService = customerService;
            _userService = userService;
        }

        [HttpPost("CreateCustomer")]
        public async Task<IActionResult> CreateCustomer(CreateCustomerRequestModel model)
        {
            var customer = await _customerService.CreateCustomer(model);
            if (customer.Success == true)
            {
                return Content(customer.Message);
            }
            return Content(customer.Message);
        }

        [HttpGet("GetAllCustomers")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _customerService.GetAll();
            if(customers.Success == true)
            {
                return Ok(customers);
            }
            return BadRequest(customers);
        }

        [HttpGet("GetCustomer/{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var customer = await _customerService.GetCustomer(id);
            if(customer.Success == true)
            {
                return Ok(customer);
            }
            return BadRequest(customer);
        }


        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordRequestModel model)
        {
            var customer = await _customerService.ForgetPassword(model);
            if(customer.Success == true)
            {
                return Content(customer.Message);
            }
            return Content(customer.Message);
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequestModel model)
        {
            var customer = await _customerService.ResetPassword(model);
            if(customer.Success == true)
            {
                return Content(customer.Message);
            }
            return Content(customer.Message);
        }
        
    }
}
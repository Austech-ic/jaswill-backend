using CMS_appBackend.DTOs.RequestModels;
using CMS_appBackend.Interface.Repositories;
using CMS_appBackend.Interface.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

using System.Security.Claims;
using System.Web;

using System.Diagnostics;

namespace CMS_appBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IUserService _userService;

        public AdminController(IAdminService adminService, IUserService userService)
        {
            _adminService = adminService;
            _userService = userService;
        }

        [HttpPost("SignUpAdmin")]
        public async Task<IActionResult> SignUpAdmin(CreateAdminRequestModel model)
        {
            var admin = await _adminService.AddAdmin(model);
            if (admin.Success == true)
            {
                return Ok(admin.Message);
            }
            return BadRequest(admin.Message);
        }

        [HttpPost("AddUserRole")]
        public async Task<IActionResult> AddUserRole(int UserId, int RoleId)
        {
            var userRole = await _adminService.AddUserRole(UserId, RoleId);
            if (userRole.Success == true)
            {
                return Ok(userRole.Message);
            }
            return BadRequest(userRole.Message);
        }

        [HttpDelete("DeleteAdmin/{id}")]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            var admin = await _adminService.DeleteAdmin(id);
            if (admin.Success == true)
            {
                return Ok(admin.Message);
            }
            return BadRequest(admin.Message);
        }

        [HttpGet("GetAllAdmins")]
        public async Task<IActionResult> GetAllAdmins()
        {
            var admins = await _adminService.GetAllAdmin();
            if(admins.Success == true)
            {
                return Ok(admins);
            }
            return BadRequest(admins);
        }

        [HttpGet("ApproveAdmin/{id}")]
        public async Task<IActionResult> ApproveAdmin(int id)
        {
            var admin = await _adminService.ApproveAdmin(id);
            if (admin.Success == true)
            {
                return Ok(admin);
            }
            return BadRequest(admin);
        }

        [HttpPost("LoginAdmin")]
        public async Task<IActionResult> LoginAdmin(LoginRequestModel model)
        {
            var login = await _userService.Login(model);
            if (login.Success == false)
            {
                return BadRequest("Email or Password does not exist ");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, (login.Data.Id).ToString()),
                new Claim(ClaimTypes.NameIdentifier, login.Data.Email),
            };
            var claimsIdentity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme
            );
            var authenticationProperties = new AuthenticationProperties();
            var principal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                authenticationProperties
            );
            return Ok(login.Message);
        }

        [HttpPost("UpdateAdmin")]
        public async Task<IActionResult> UpdateAdmin(UpdateAdminRequestModels model)
        {
            var admin = await _adminService.UpdateAdmin(model);
            if (admin.Success == true)
            {
                return Ok(admin.Message);
            }
            return BadRequest(admin.Message);
        }

        [HttpPost("UpdateAdminPassword")]
        public async Task<IActionResult> UpdateAdminPassword(ChangePasswordRequestModel model, int id)
        {
            var admin = await _adminService.ChangePassword(model, id);
            if (admin.Success == true)
            {
                return Ok(admin.Message);
            }
            return BadRequest(admin.Message);
        }

        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordRequestModel model)
        {
            var admin = await _adminService.ForgetPassword(model);
            if (admin.Success == true)
            {
                return Content(admin.Message);
            }
            return Content(admin.Message);
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequestModel model)
        {
            var admin = await _adminService.ResetPassword(model);
            if (admin.Success == true)
            {
                return Content(admin.Message);
            }
            return Content(admin.Message);
        }

        [HttpGet("GetAdminUserNameAndEmail/{id}")]
        public async Task<IActionResult> GetAdminUserNameAndEmail(int id)
        {
            var admin = await _adminService.GetAdminUserNameAndEmail(id);
            if (admin.Success == true)
            {
                return Ok(admin);
            }
            return BadRequest(admin);
        }

        [HttpGet("Logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }

        
    }
}

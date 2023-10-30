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

        [HttpPost("CreateAdmin")]
        public async Task<IActionResult> CreateAdmin(CreateAdminRequestModel model)
        {
            var admin = await _adminService.AddAdmin(model);
            if (admin.Success == true)
            {
                return Content(admin.Message);
            }
            return Content(admin.Message);
        }

        [HttpPost("AddUserRole")]
        public async Task<IActionResult> AddUserRole(int UserId, int RoleId)
        {
            var userRole = await _adminService.AddUserRole(UserId, RoleId);
            if (userRole.Success == true)
            {
                return Content(userRole.Message);
            }
            return Content(userRole.Message);
        }

        [HttpDelete("DeleteAdmin/{id}")]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            var admin = await _adminService.DeleteAdmin(id);
            if (admin.Success == true)
            {
                return Content(admin.Message);
            }
            return Content(admin.Message);
        }

        [HttpGet("GetAllAdmins")]
        public async Task<IActionResult> GetAllAdmins()
        {
            var admins = await _adminService.GetAllAdmin();
            if(admins.Success == true)
            {
                return Content(admins.Message);
            }
            return Content(admins.Message);
        }

        [HttpGet("ApproveAdmin/{id}")]
        public async Task<IActionResult> ApproveAdmin(int id)
        {
            var admin = await _adminService.ApproveAdmin(id);
            if (admin.Success == true)
            {
                return Content(admin.Message);
            }
            return Content(admin.Message);
        }

        [HttpPost("LoginAdmin")]
        public async Task<IActionResult> LoginAdmin(string email, string password)
        {
            var login = await _userService.Login(email, password);
            if (login.Success == false)
            {
                return Content("Email or Password does not exist ");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, (login.Data.Id).ToString()),
                new Claim(ClaimTypes.NameIdentifier, login.Data.Email),
                new Claim(ClaimTypes.NameIdentifier, login.Data.Password),
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
            return Content(login.Message);
        }

        [HttpPost("UpdateAdmin")]
        public async Task<IActionResult> UpdateAdmin(UpdateAdminRequestModels model)
        {
            var admin = await _adminService.UpdateAdmin(model);
            if (admin.Success == true)
            {
                return Content(admin.Message);
            }
            return Content(admin.Message);
        }

        [HttpGet("Logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}

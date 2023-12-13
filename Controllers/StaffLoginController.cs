using CMS_appBackend.DTOs;
using CMS_appBackend.Implementations.Services;
using CMS_appBackend.Implementations.Repositories;
using CMS_appBackend.Context;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using CMS_appBackend.DTOs.RequestModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Web;

using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using CMS_appBackend.Interface.Services;
using CMS_appBackend.Interface.Repositories;

namespace CMS_appBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffLoginController : Controller
    {
        private readonly ApplicationContext dbContext;

        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public StaffLoginController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        [HttpPost("LoginStaff")]
        public async Task<IActionResult> LoginStaff(LoginRequestModel model)
        {
            var login = await _userService.Login(model);
            var role = await _roleService.GetRoleByUserId(login.Data.Id);

            if (login == null)
            {
                return Content("Email or Password does not exist ");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, login.Data.Email),
                new Claim(ClaimTypes.NameIdentifier, login.Data.Password)
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
            return Ok();
        }

        
    }
}

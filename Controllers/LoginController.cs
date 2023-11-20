using CMS_appBackend.DTOs;
using CMS_appBackend.Implementations.Services;
using CMS_appBackend.Implementations.Repositories;
using CMS_appBackend.Context;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
    public class LoginController : Controller
    {
        private readonly ApplicationContext dbContext;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public LoginController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            var login = await _userService.Login(email, password);
            var role = await _roleService.GetRoleByUserId(login.Data.Id);
            if (login.Success == false)
            {
                return Content("Email or Password does not exist ");
            }
            else if (login.Success == true && role == null)
            {
                return Ok();
            }
            else if (login.Success == true && role.Success == true)
            {
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
                //var role = await _roleService.GetRoleByUserId(login.Data.Id);
                if (role.Data.Name == "Admin")
                {
                    return Ok();
                }
            }
            return Content(login.Message);
        }

        [HttpGet("Logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}

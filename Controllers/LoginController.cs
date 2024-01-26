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
using CMS_appBackend.Authentication;

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
        private readonly IJWTAuthentication _auth;

        public LoginController(IUserService userService, IRoleService roleService, IJWTAuthentication auth)
        {
            _userService = userService;
            _roleService = roleService;
            _auth = auth;
        }

        [HttpPost("Login")]
         public async Task<IActionResult> LoginAdmin(LoginRequestModel model)
        {            
            var login = await _userService.Login(model);
            if (!login.Success)
            {
                return BadRequest(login);
            }
            var token = _auth.GenerateToken(login);
            return Ok(
                new
                {
                    Message = login.Message,
                    Data = login.Data,
                    Token = token
                }
            );
        }

        [HttpGet("Logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}

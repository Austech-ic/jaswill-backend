using CMS_appBackend.DTOs.RequestModels;
using CMS_appBackend.Interface.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace CMS_appBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRole(CreateRoleRequestmodel model)
        {
            var role = await _roleService.AddRoleAsync(model);
            if (role.Success == true)
            {
                return Content(role.Message);
            }
            return Content(role.Message);
        }

        [HttpPost("UpdateRole")]
        public async Task<IActionResult> UpdateRole(UpdateUserRoleRequestModel model)
        {
            var role = await _roleService.UpdateUserRole(model);
            if (role.Success == true)
            {
                return Content(role.Message);
            }
            return Content(role.Message);
        }


        [HttpGet("ViewAllRoles")]
        public async Task<IActionResult> ViewAllRoles()
        {
            var roles = await _roleService.GetAllRoleAsync();
            return View(roles);
        }
    }
}

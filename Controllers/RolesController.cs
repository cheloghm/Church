using Church.ServiceInterfaces;
using Church.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Church.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;

        public RolesController(IRoleService roleService, IUserService userService)
        {
            _roleService = roleService;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            if (await _roleService.RoleExists(roleName))
                return BadRequest("Role already exists");

            var role = await _roleService.CreateRole(roleName);
            return Ok(role);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(string id)
        {
            var role = await _roleService.GetRoleById(id);

            if (role == null)
                return NotFound();

            return Ok(role);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _roleService.GetAllRoles();
            return Ok(roles);
        }

        [HttpPut("{id}/changeRole")]
        public async Task<IActionResult> ChangeUserRole(string id, string newRoleId)
        {
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (currentUserId == null)
                return Unauthorized();

            var isSuccess = await _userService.ChangeUserRole(id, newRoleId, currentUserId);

            if (!isSuccess)
                return BadRequest("Failed to change user role");

            return Ok();
        }
    }
}

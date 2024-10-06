using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BaristaXpertControl.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        // Danh sách các vai trò hợp lệ
        private readonly List<string> _validRoles = new List<string> { "Admin", "Manager", "Employee" };

        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        // Tạo vai trò mới
        [HttpPost("create")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
                return BadRequest("Role name must not be empty.");

            // Kiểm tra vai trò có hợp lệ không
            if (!_validRoles.Contains(roleName))
                return BadRequest("Invalid role. Only 'Admin', 'Manager', and 'Employee' roles are allowed.");

            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (roleExists)
                return Conflict("Role already exists.");

            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));

            if (result.Succeeded)
                return Ok($"Role '{roleName}' created successfully.");
            else
                return BadRequest(result.Errors);
        }

        // Lấy danh sách tất cả vai trò
        [HttpGet("get-all")]
        public IActionResult GetAllRoles()
        {
            var roles = _roleManager.Roles;
            return Ok(roles);
        }

        // Xóa vai trò
        [HttpDelete("delete/{roleName}")]
        public async Task<IActionResult> DeleteRole(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);

            if (role == null)
                return NotFound("Role not found.");

            var result = await _roleManager.DeleteAsync(role);

            if (result.Succeeded)
                return Ok($"Role '{roleName}' deleted successfully.");
            else
                return BadRequest(result.Errors);
        }

        // Cập nhật tên vai trò
        [HttpPut("update")]
        public async Task<IActionResult> UpdateRole(string currentRoleName, string newRoleName)
        {
            var role = await _roleManager.FindByNameAsync(currentRoleName);

            if (role == null)
                return NotFound("Role not found.");

            role.Name = newRoleName;
            var result = await _roleManager.UpdateAsync(role);

            if (result.Succeeded)
                return Ok($"Role '{currentRoleName}' updated to '{newRoleName}'.");
            else
                return BadRequest(result.Errors);
        }
    }
}

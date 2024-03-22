using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gradTrackerAPI.Identity;
using gradTrackerAPI.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace gradTrackerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {

        private readonly RoleManager<AppRole> _roleManager;

        public RolesController(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpPost("addrole")]
        private async Task<IActionResult> AddRole(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                return BadRequest("Role name cannot be empty.");
            }

            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (roleExists)
            {
                return BadRequest("Role already exists.");
            }

            var result = await _roleManager.CreateAsync(new AppRole{
                Name = roleName,
                ConcurrencyStamp = DateTime.UtcNow.ToString()
            });
            if (result.Succeeded)
            {
                return Ok($"Role '{roleName}' created successfully.");
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "500", Message = string.Join("; ", result.Errors.Select(error => error.Description)) });
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using gradTrackerAPI.Identity;
using gradTrackerAPI.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace gradTrackerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly ILogger<AuthenticationController> _logger;

        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthenticationController(ILogger<AuthenticationController> logger, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IConfiguration configuration)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost("RegisterUser")]
        public async Task<IActionResult> Register(UserRegistrationViewModel reg)
        {
            //check if user exist by email
            var userExist = await _userManager.FindByEmailAsync(reg.Email);

            if (userExist != null)
                return StatusCode(StatusCodes.Status403Forbidden, new Response { Status = "403", Message = "User already exists!" });

            //create new user
            AppUser user = new ()
            {
                Email = reg.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = reg.Username,
                Password = reg.Password,
            };


            var result = await _userManager.CreateAsync(user, reg.Password);
            if (result.Succeeded)
            {
                return StatusCode(StatusCodes.Status201Created, new Response { Status = "Success", Message = "User created successfully." });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "500", Message = string.Join("; ", result.Errors.Select(error => error.Description)) });
            }

        }


    }
}
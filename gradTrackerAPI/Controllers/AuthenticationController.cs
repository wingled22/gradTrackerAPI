using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using gradTrackerAPI.Identity;
using gradTrackerAPI.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

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
                return StatusCode(StatusCodes.Status403Forbidden, new Response { Status = "Error", Message = "User already exists!" });

            //create new user
            AppUser user = new()
            {
                Email = reg.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = reg.Username,
                Password = reg.Password,
            };


            if (await _roleManager.RoleExistsAsync("Admin"))
            {

                var result = await _userManager.CreateAsync(user, reg.Password);
                if (!result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = string.Join("; ", result.Errors.Select(error => error.Description)) });
                }

                await _userManager.AddToRoleAsync(user, "Admin");
                return StatusCode(StatusCodes.Status201Created, new Response { Status = "Success", Message = "User created successfully." });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Role for the user creation does not exists." });
            }

        }

        [HttpPost("LoginUser")]
        public async Task<IActionResult> Login(UserLoginViewModel login)
        {
            try
            {
                //check if user exists
                var user = await _userManager.FindByNameAsync(login.Username);

                //check if user match the password and username
                if (user != null && await _userManager.CheckPasswordAsync(user, login.Password))
                {

                    // get claims
                    var authClaims = new List<Claim>{
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                    var userRoles = await _userManager.GetRolesAsync(user);
                    foreach (var role in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, role));
                    }

                    var jwtToken = GetToken(authClaims);
                    return Ok(
                        new JwtSecurityTokenHandler().WriteToken(jwtToken)
                        );

                }
                return Unauthorized();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }


        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(5),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }

    }
}
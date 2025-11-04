using E_Prescribing_API.Data.Services;
using E_Prescribing_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_Prescribing_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;
        private readonly AppSettings _appSettings;
        private readonly ILogger<AccountController> _logger;

        public AccountController(UserManager<ApplicationUser> userManager, ApplicationDbContext db, IOptions<AppSettings> appSettings, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _db = db;
            _appSettings = appSettings.Value;
            _logger = logger;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.Username) || string.IsNullOrWhiteSpace(model.Password))
                return BadRequest(new { message = "Invalid login request." });

            try
            {
                var user = await _userManager.FindByNameAsync(model.Username);
                if (user == null)
                {
                    return Unauthorized(new { message = "Invalid email or password." });
                }

                var passwordValid = await _userManager.CheckPasswordAsync(user, model.Password);
                if (!passwordValid)
                {
                    return Unauthorized(new { message = "Invalid email or password." });
                }

                var roles = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
        {
            new Claim("UserID", user.Id.ToString())
        };

                foreach (var role in roles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                var key = Encoding.UTF8.GetBytes(_appSettings.JWTSecret);
                var signingKey = new SymmetricSecurityKey(key);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(authClaims),
                    Expires = DateTime.UtcNow.AddMinutes(10),
                    SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));

                return Ok(new
                {
                    token,
                    expires = tokenDescriptor.Expires,
                    user = new
                    {
                        user.Id,
                        user.UserName,
                        roles
                    }
                });
            }
            catch (Exception ex)
            {
    
                _logger.LogError($"Login error: {ex.Message}");
                return StatusCode(500, new { message = "An error occurred while processing the login request." });
            }
        }


    }


}

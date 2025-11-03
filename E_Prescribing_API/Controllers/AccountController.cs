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
        public AccountController(UserManager<ApplicationUser> userManager, ApplicationDbContext db, IOptions<AppSettings> appSettings)
        {
            _userManager = userManager;
            _db = db;
            _appSettings = appSettings.Value;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.EmailAddress);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var signInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWTSecret));

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                 new Claim("UserID", user.Id.ToString())
             }),
                    Expires = DateTime.UtcNow.AddMinutes(10),
                    SigningCredentials = new SigningCredentials(signInKey, SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);

                return Ok(new { token });
            }

            return BadRequest(new { message = "Username or password is incorrect." });
        }
  
    }


}

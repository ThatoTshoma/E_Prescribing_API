using E_Prescribing_API.CollectionModel;
using E_Prescribing_API.Data.Services;
using E_Prescribing_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IEmailSender _emailSender;
        private readonly IPasswordGenerator _passwordGenerator;
        private readonly UsernameGenerator _usernameGenerator;

        public AccountController(UserManager<ApplicationUser> userManager, ApplicationDbContext db, IOptions<AppSettings> appSettings, ILogger<AccountController> logger, IEmailSender emailSender, IPasswordGenerator passwordGenerator, UsernameGenerator usernameGenerator)
        {
            _userManager = userManager;
            _db = db;
            _appSettings = appSettings.Value;
            _logger = logger;
            _emailSender = emailSender;
            _passwordGenerator = passwordGenerator;
            _usernameGenerator = usernameGenerator;
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterCollection model)
        {
            try
            {
                if (model == null || model.ApplicationUser == null)
                    return BadRequest("Invalid user data.");

                var existingUser = await _userManager.FindByEmailAsync(model.ApplicationUser.Email);
                if (existingUser != null)
                    return BadRequest("A user with this email already exists.");

                var password = _passwordGenerator.GenerateRandomPassword();
                var user = new ApplicationUser
                {
                    UserName = _usernameGenerator.GenerateUserName(model.ApplicationUser.Email),
                    Email = model.ApplicationUser.Email
                };

                var result = await _userManager.CreateAsync(user, password);
                if (!result.Succeeded)
                {
                    _logger.LogWarning("User creation failed: {Errors}", string.Join(", ", result.Errors));
                    return BadRequest("User creation failed. Check input and roles.");
                }

                await _userManager.AddToRoleAsync(user, model.Role);

                switch (model.Role)
                {
                    case "Nurse":
                        _db.Nurses.Add(new Nurse
                        {
                            Name = model.Nurse.Name,
                            Surname = model.Nurse.Surname,
                            FullName = model.Nurse.Name + " " + model.Nurse.Surname,
                            ContactNumber = model.Nurse.ContactNumber,
                            RegistrationNumber = model.Nurse.RegistrationNumber,
                            UserId = user.Id
                        });
                        break;

                    case "Pharmacist":
                        _db.Pharmacists.Add(new Pharmacist
                        {
                            Name = model.Pharmacist.Name,
                            Surname = model.Pharmacist.Surname,
                            FullName = model.Pharmacist.Name + " " + model.Pharmacist.Surname,
                            ContactNumber = model.Pharmacist.ContactNumber,
                            RegistrationNumber = model.Pharmacist.RegistrationNumber,
                            UserId = user.Id
                        });
                        break;

                    case "Surgeon":
                        _db.Surgeons.Add(new Surgeon
                        {
                            Name = model.Surgeon.Name,
                            Surname = model.Surgeon.Surname,
                            FullName = model.Surgeon.Name + " " + model.Surgeon.Surname,
                            ContactNumber = model.Surgeon.ContactNumber,
                            RegistrationNumber = model.Surgeon.RegistrationNumber,
                            UserId = user.Id
                        });
                        break;

                    case "Anaesthesiologist":
                        _db.Anaesthesiologists.Add(new Anaesthesiologist
                        {
                            Name = model.Anaesthesiologist.Name,
                            Surname = model.Anaesthesiologist.Surname,
                            FullName = model.Anaesthesiologist.Name + " " + model.Anaesthesiologist.Surname,
                            ContactNumber = model.Anaesthesiologist.ContactNumber,
                            RegistrationNumber = model.Anaesthesiologist.RegistrationNumber,
                            UserId = user.Id
                        });
                        break;
                }
                await _db.SaveChangesAsync();

                try
                {
                    await _emailSender.SendEmailAsync(
                        user.Email,
                        "User Credentials",
                        $@"
                        <p>Dear {model.Role},</p>
                        <p>Your account has been created:</p>
                        <ul>
                            <li><strong>Username:</strong> {user.UserName}</li>
                            <li><strong>Password:</strong> {password}</li>
                        </ul>
                        <p>Kind regards,</p> 
                        <p>Admin</p>"
                    );
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to send email to {Email}", user.Email);
                }

                return Ok(new
                {
                    message = "User created successfully",
                    username = user.UserName,
                    email = user.Email,
                    role = model.Role
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding user.");
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }
        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser()
        {
            try
            {
                var users = await _userManager.Users.ToListAsync();
                var userList = new List<object>();

                foreach (var user in users)
                {
                    var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

                    if (role == "Nurse")
                    {
                        var nurse = await _db.Nurses.FirstOrDefaultAsync(n => n.UserId == user.Id);

                        if (nurse != null)
                        {
                            userList.Add(new
                            {
                                nurse.Name,
                                nurse.Surname,
                                nurse.FullName,
                                nurse.ContactNumber,
                                nurse.RegistrationNumber,
                                nurse.UserId,
                                Role = role,
                                Username = user.UserName,
                                UserEmail = user.Email
                            });
                        }
                    }
                    else if (role == "Pharmacist")
                    {
                        var pharmacist = await _db.Pharmacists.FirstOrDefaultAsync(p => p.UserId == user.Id);

                        if (pharmacist != null)
                        {
                            userList.Add(new
                            {
                                pharmacist.Name,
                                pharmacist.Surname,
                                pharmacist.FullName,
                                pharmacist.ContactNumber,
                                pharmacist.RegistrationNumber,
                                pharmacist.UserId,
                                Role = role,
                                Username = user.UserName,
                                UserEmail = user.Email
                            });
                        }
                    }
                    else if (role == "Surgeon")
                    {
                        var surgeon = await _db.Surgeons.FirstOrDefaultAsync(s => s.UserId == user.Id);

                        if (surgeon != null)
                        {
                            userList.Add(new
                            {
                                surgeon.Name,
                                surgeon.Surname,
                                surgeon.FullName,
                                surgeon.ContactNumber,
                                surgeon.RegistrationNumber,
                                surgeon.UserId,
                                Role = role,
                                Username = user.UserName,
                                UserEmail = user.Email
                            });
                        }
                    }
                    else if (role == "Anaesthesiologist")
                    {
                        var anaesthesiologist = await _db.Anaesthesiologists.FirstOrDefaultAsync(a => a.UserId == user.Id);

                        if (anaesthesiologist != null)
                        {
                            userList.Add(new
                            {
                                anaesthesiologist.Name,
                                anaesthesiologist.Surname,
                                anaesthesiologist.FullName,
                                anaesthesiologist.ContactNumber,
                                anaesthesiologist.RegistrationNumber,
                                anaesthesiologist.UserId,
                                Role = role,
                                Username = user.UserName,
                                UserEmail = user.Email
                            });
                        }
                    }
                }

                return Ok(userList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting user.");
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginCollection model)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.Username) || string.IsNullOrWhiteSpace(model.Password))
                return BadRequest(new { message = "Invalid login request." });

            try
            {
                var user = await _userManager.FindByNameAsync(model.Username.Trim());
                if (user == null)
                {
                    return Unauthorized(new { message = "Invalid email or password." });
                }

                if (user.LockoutEnd.HasValue && user.LockoutEnd > DateTimeOffset.UtcNow)
                {
                    return Unauthorized(new { message = "Your account has been deactivated. Please contact the administrator." });
                }

                var passwordValid = await _userManager.CheckPasswordAsync(user, model.Password.Trim());
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

        [HttpPost("DeactivateUser/{id}")]
        public async Task<IActionResult> DeactivateUser(int id)
        {
            try
            {

                var user = await _userManager.FindByIdAsync(id.ToString());
                if (user == null)
                    return NotFound("User not found.");

                if (!user.LockoutEnabled && user.LockoutEnd == DateTimeOffset.MaxValue)
                    return BadRequest("User is already deactivated.");

                user.LockoutEnabled = true;
                user.LockoutEnd = DateTimeOffset.MaxValue;

                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    _logger.LogWarning("Failed to deactivate user {UserId}: {Errors}", id, string.Join(", ", result.Errors));
                    return BadRequest("Failed to deactivate user.");
                }

                _logger.LogInformation("User {UserId} has been deactivated.", id);

                return Ok(new
                {
                    message = "User has been deactivated successfully.",
                    userId = id,
                    email = user.Email
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deactivating user with Id {UserId}", id);
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }
        [HttpPost("ReactivateUser/{id}")]
        public async Task<IActionResult> ReactivateUser(int id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id.ToString());
                if (user == null)
                    return NotFound("User not found.");

                user.LockoutEnabled = false;
                user.LockoutEnd = null;

                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    _logger.LogWarning("Failed to reactivate user {UserId}: {Errors}", id, string.Join(", ", result.Errors));
                    return BadRequest("Failed to reactivate user.");
                }

                _logger.LogInformation("User {UserId} has been reactivated.", id);

                return Ok(new
                {
                    message = "User has been reactivated successfully.",
                    userId = id,
                    email = user.Email
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while reactivating user with Id {UserId}", id);
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCollection model)
        {
            if (string.IsNullOrWhiteSpace(model.Email))
                return BadRequest("Email is required.");

            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                    return NotFound("No user associated with this email.");

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                var encodedToken = System.Net.WebUtility.UrlEncode(token);

                var resetUrl = $"https://localhost:7150/reset-password?email={user.Email}&token={encodedToken}";

                await _emailSender.SendEmailAsync(
                    user.Email,
                    "Password Reset Request",
                    $@"
                    <p>Dear User,</p>
                    <p>You requested to reset your password. Click the link below to reset it:</p>
                    <p><a href='{resetUrl}'>Reset Password</a></p>
                    <p>If you did not request this, please ignore this email.</p>
                    <p>Kind regards,</p>
                    <p>Admin</p>"
                        );

                return Ok(new { message = "Password reset email sent successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while processing forgot password for {Email}", model.Email);
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCollection model)
        {
            if (string.IsNullOrWhiteSpace(model.Email) ||string.IsNullOrWhiteSpace(model.Token) ||string.IsNullOrWhiteSpace(model.NewPassword))
                return BadRequest("Invalid reset password request.");

            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                    return NotFound("User not found.");

                var decodedToken = System.Net.WebUtility.UrlDecode(model.Token);

                var result = await _userManager.ResetPasswordAsync(user, decodedToken, model.NewPassword);
                if (!result.Succeeded)
                {
                    _logger.LogWarning("Failed to reset password for {Email}: {Errors}", model.Email, string.Join(", ", result.Errors));
                    return BadRequest("Password reset failed. The token might be invalid or expired.");
                }

                return Ok(new { message = "Password has been reset successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while resetting password for {Email}", model.Email);
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }
        [HttpPost("AdminResetPassword/{id}")]
        public async Task<IActionResult> AdminResetPassword(int id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id.ToString());
                if (user == null)
                    return NotFound("User not found.");

                var newPassword = _passwordGenerator.GenerateRandomPassword();
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
                if (!result.Succeeded)
                {
                    _logger.LogWarning("Failed to reset password for user {UserId}: {Errors}", id, string.Join(", ", result.Errors));
                    return BadRequest("Password reset failed.");
                }

                await _emailSender.SendEmailAsync(
                    user.Email,
                    "Password Reset by Admin",
                    $@"
                    <p>Dear User,</p>
                    <p>Your password has been reset by the administrator.</p>
                    <p><strong>New Password:</strong> {newPassword}</p>
                    <p>Kind regards,</p>
                    <p>Admin</p>"
                );

                return Ok(new { message = "Password reset successfully.", user = user.Email });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while resetting password for user {UserId}", id);
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

    }


}

using E_Prescribing_API.CollectionModel;
using E_Prescribing_API.Data.Services;
using E_Prescribing_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace E_Prescribing_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;
        private readonly IEmailSender _emailSender;
        private readonly IPasswordGenerator _passwordGenerator;
        private readonly UsernameGenerator _usernameGenerator;
        private readonly ILogger<AdminController> _logger;

        public AdminController(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext db,
            IEmailSender emailSender,
            IPasswordGenerator passwordGenerator,
            UsernameGenerator usernameGenerator,
            ILogger<AdminController> logger)
        {
            _userManager = userManager;
            _db = db;
            _emailSender = emailSender;
            _passwordGenerator = passwordGenerator;
            _usernameGenerator = usernameGenerator;
            _logger = logger;
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser(UserCollection model)
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
                    Email = model.ApplicationUser.Email,
                    UserRole = model.ApplicationUser.UserRole
                };

                var result = await _userManager.CreateAsync(user, password);
                if (!result.Succeeded)
                {
                    _logger.LogWarning("User creation failed: {Errors}", string.Join(", ", result.Errors));
                    return BadRequest("User creation failed. Check input and roles.");
                }

                await _userManager.AddToRoleAsync(user, user.UserRole);

                string fullName;

                if (model.ApplicationUser.UserRole == "Nurse")
                {
                    fullName = $"{model.Nurse.Name} {model.Nurse.Surname}";
                }
                else if (model.ApplicationUser.UserRole == "Pharmacist")
                {
                    fullName = $"{model.Pharmacist.Name} {model.Pharmacist.Surname}";
                }
                else if (model.ApplicationUser.UserRole == "Surgeon")
                {
                    fullName = $"{model.Surgeon.Name} {model.Surgeon.Surname}";
                }
                else if (model.ApplicationUser.UserRole == "Anaesthesiologist")
                {
                    fullName = $"{model.Anaesthesiologist.Name} {model.Anaesthesiologist.Surname}";
                }
                else
                {
                    fullName = "User";
                }

                switch (user.UserRole)
                {
                    case "Nurse":
                        _db.Nurses.Add(new Nurse
                        {
                            Name = model.Nurse.Name,
                            Surname = model.Nurse.Surname,
                            FullName = fullName,
                            ContactNumber = model.Nurse.ContactNumber,
                            EmailAddress = model.ApplicationUser.Email,
                            RegistrationNumber = model.Nurse.RegistrationNumber,
                            UserId = user.Id
                        });
                        break;

                    case "Pharmacist":
                        _db.Pharmacists.Add(new Pharmacist
                        {
                            Name = model.Pharmacist.Name,
                            Surname = model.Pharmacist.Surname,
                            FullName = fullName,
                            ContactNumber = model.Pharmacist.ContactNumber,
                            EmailAddress = model.ApplicationUser.Email,
                            RegistrationNumber = model.Pharmacist.RegistrationNumber,
                            UserId = user.Id
                        });
                        break;

                    case "Surgeon":
                        _db.Surgeons.Add(new Surgeon
                        {
                            Name = model.Surgeon.Name,
                            Surname = model.Surgeon.Surname,
                            FullName = fullName,
                            ContactNumber = model.Surgeon.ContactNumber,
                            EmailAddress = model.ApplicationUser.Email,
                            RegistrationNumber = model.Surgeon.RegistrationNumber,
                            UserId = user.Id
                        });
                        break;

                    case "Anaesthesiologist":
                        _db.Anaesthesiologists.Add(new Anaesthesiologist
                        {
                            Name = model.Anaesthesiologist.Name,
                            Surname = model.Anaesthesiologist.Surname,
                            FullName = fullName,
                            ContactNumber = model.Anaesthesiologist.ContactNumber,
                            EmailAddress = model.ApplicationUser.Email,
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
                        <p>Dear {fullName},</p>
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
                    role = user.UserRole
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding user.");
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }
    }
}

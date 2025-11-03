using E_Prescribing_API.CollectionModel;
using E_Prescribing_API.Data.Services;
using E_Prescribing_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;

namespace E_Prescribing_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;
        private readonly IEmailSender _emailSender;
        public readonly IPasswordGenerator _passwordGenerator ;
        public readonly UsernameGenerator _usernameGenerator;

        public AdminController(UserManager<ApplicationUser> userManager, ApplicationDbContext db, IEmailSender emailSender, IPasswordGenerator passwordGenerator, UsernameGenerator usernameGenerator)
        {
            _userManager = userManager;
            _db = db;
            _emailSender = emailSender;
            _passwordGenerator = passwordGenerator;
            _usernameGenerator = usernameGenerator;
        }

        public IActionResult AddUser()
        {
            var userCollection = new UserCollection();

            return Ok(userCollection);
        }
        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser(UserCollection model)
        {
            var existingUser = await _userManager.FindByEmailAsync(model.ApplicationUser.Email);
            if (existingUser != null)
            {
                BadRequest("A user with this email already exists.");
                return Ok(model);
            }
            var generatedPassword = _passwordGenerator.GenerateRandomPassword();


            var user = new ApplicationUser
            {
                UserName = _usernameGenerator.GenerateUserName(model.ApplicationUser.Email),
                Email = model.ApplicationUser.Email,
                UserRole = model.ApplicationUser.UserRole
            };

            var result = await _userManager.CreateAsync(user, generatedPassword);
            string userFirstName;

            if (model.ApplicationUser.UserRole == "Nurse")
                userFirstName = model.Nurse.Name + " " + model.Nurse.Surname;
            else if (model.ApplicationUser.UserRole == "Pharmacist")
                userFirstName = model.Pharmacist.Name + " " + model.Pharmacist.Surname;
            else if (model.ApplicationUser.UserRole == "Surgeon")
                userFirstName = model.Surgeon.Name + " " + model.Surgeon.Surname;
            else if (model.ApplicationUser.UserRole == "Anaesthesiologist")
                userFirstName = model.Anaesthesiologist.Name + " " + model.Anaesthesiologist.Surname;
            else
                userFirstName = "User";


            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, user.UserRole);

                _emailSender.SendEmail(user.Email,
                            "User Credentials",
                            $@"
                            <p>Dear {userFirstName},</p>
                            <p>Welcome to E-Prescribing! We are excited to have you on board. Below are your login credentials:</p>
                            <ul>
                                <li><strong>Username:</strong> {user.UserName}</li>
                                <li><strong>Password:</strong> {generatedPassword}</li>
                            </ul>
                            <p>If you encounter any issues, feel free to reach out to our support team at mycode1997@gmail.com.</p>
                            <p>Kind regards,</p>
                            <p>The E-Prescribing Admin</p>"

                );

                if (user.UserRole == "Nurse")
                {
                    var nurse = new Nurse
                    {
                        Name = model.Nurse.Name,
                        Surname = model.Nurse.Surname,
                        FullName = model.Nurse.Name + " " + model.Nurse.Surname,
                        ContactNumber = model.Nurse.ContactNumber,
                        EmailAddress = model.ApplicationUser.Email,
                        RegistrationNumber = model.Nurse.RegistrationNumber,
                        UserId = user.Id,
                    };
                    _db.Nurses.Add(nurse);
                }
                else if (user.UserRole == "Pharmacist")
                {
                    var pharmacist = new Pharmacist
                    {
                        Name = model.Pharmacist.Name,
                        Surname = model.Pharmacist.Surname,
                        FullName = model.Pharmacist.Name + " " + model.Pharmacist.Surname,
                        ContactNumber = model.Pharmacist.ContactNumber,
                        EmailAddress = model.ApplicationUser.Email,
                        RegistrationNumber = model.Pharmacist.RegistrationNumber,
                        UserId = user.Id,
                    };
                    _db.Pharmacists.Add(pharmacist);
                }
                else if (user.UserRole == "Surgeon")
                {
                    var surgeon = new Surgeon
                    {
                        Name = model.Surgeon.Name,
                        Surname = model.Surgeon.Surname,
                        FullName = model.Surgeon.Name + " " + model.Surgeon.Surname,
                        ContactNumber = model.Surgeon.ContactNumber,
                        EmailAddress = model.ApplicationUser.Email,
                        RegistrationNumber = model.Surgeon.RegistrationNumber,
                        UserId = user.Id,
                    };
                    _db.Surgeons.Add(surgeon);
                }
                else if (user.UserRole == "Anaesthesiologist")
                {
                    var anaesthesiologist = new Anaesthesiologist
                    {
                        Name = model.Anaesthesiologist.Name,
                        Surname = model.Anaesthesiologist.Surname,
                        FullName = model.Anaesthesiologist.Name + " " + model.Anaesthesiologist.Surname,
                        ContactNumber = model.Anaesthesiologist.ContactNumber,
                        EmailAddress = model.ApplicationUser.Email,
                        RegistrationNumber = model.Anaesthesiologist.RegistrationNumber,
                        UserId = user.Id,
                    };
                    _db.Anaesthesiologists.Add(anaesthesiologist);
                }

                await _db.SaveChangesAsync();
                return RedirectToAction("ListUser");

            }
            else
                return BadRequest();
        }

    }
}

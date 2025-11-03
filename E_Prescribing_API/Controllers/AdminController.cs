using E_Prescribing_API.CollectionModel;
using E_Prescribing_API.Data.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_Prescribing_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;
        private readonly IEmailSender _emailSender;

        public AdminController(UserManager<ApplicationUser> userManager, ApplicationDbContext db, IEmailSender emailSender)
        {
            _userManager = userManager;
            _db = db;
            _emailSender = emailSender;
        }


        public IActionResult AddUser()
        {
            var userCollection = new UserCollection();

            return BadRequest(new { message = "Username or password is incorrect." });
        }


    }
}

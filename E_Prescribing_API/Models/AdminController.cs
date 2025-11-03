using E_Prescribing_API.Data.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Prescribing_API.Models
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;
        private readonly IMyEmailSender _myEmailSender;

        public AdminController(UserManager<ApplicationUser> userManager, ApplicationDbContext db, IMyEmailSender myEmailSender)
        {
            _userManager = userManager;
            _db = db;
            _myEmailSender = myEmailSender;
        }
    }
}

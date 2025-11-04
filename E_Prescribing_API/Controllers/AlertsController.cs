using E_Prescribing_API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Prescribing_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlertsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<AlertsController> _logger;

        public AlertsController(ApplicationDbContext db, ILogger<AlertsController> logger)
        {
            _db = db;
            _logger = logger;
        }

    }
}

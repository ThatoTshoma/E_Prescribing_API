using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Prescribing_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LandingController : ControllerBase
    {
        [HttpGet("GetNurse")]
        [Authorize(Roles = "Nurse")]
        public string GetNurse()
        {
            return "Nurse page";
        }

    }
}

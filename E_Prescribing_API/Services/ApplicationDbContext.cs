using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace E_Prescribing_API.Services
{
    public class ApplicationUser : IdentityUser<int>
    {

    }
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
    }
}

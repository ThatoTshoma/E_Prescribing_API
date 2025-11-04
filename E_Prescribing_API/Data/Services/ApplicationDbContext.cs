using E_Prescribing_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_Prescribing_API.Data.Services
{
    public class ApplicationUser : IdentityUser<int>
    {
    }
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Suburb> Suburbs { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<FacilityType> FacilitiesTypes { get; set; }
        public DbSet<MedicalSaff> MedicalStaffs { get; set; }
    }
}

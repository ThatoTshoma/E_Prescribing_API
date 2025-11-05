using E_Prescribing_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_Prescribing_API.Data
{
    public class ApplicationUser : IdentityUser<int>
    {
    }
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Suburb> Suburbs { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<FacilityType> FacilitiesTypes { get; set; }
        public DbSet<MedicalStaff> MedicalStaffs { get; set; }
        public DbSet<ActiveIngredient> ActiveIngredients { get; set; }
        public DbSet<ConditionDiagnosis> ConditionDiagnosis { get; set; }
        public DbSet<ContraIndication> ContraIndications { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<DosageForm> DosageForms { get; set; }
        public DbSet<MedicationIngredient> MedicationIngredients { get; set; }
        public DbSet<PatientMedication> PatientMedications { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientCondition> PatientConditions { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<PrescribedMedication> PrescribedMedications { get;set; }
        public DbSet<PatientAllergy> PatientAllergies { get; set; }
        public DbSet<MedicationInteraction> MedicationInteractions { get; set; }
    }
}

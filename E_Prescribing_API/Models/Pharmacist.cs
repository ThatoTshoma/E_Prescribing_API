using E_Prescribing_API.Data.Services;
using System.ComponentModel.DataAnnotations;

namespace E_Prescribing_API.Models
{
    public class Pharmacist
    {
        [Key]
        public int PharmacistId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; }

        [Display(Name = "Registration Number")]
        public string RegistrationNumber { get; set; }
        public ApplicationUser User { get; set; }
        public int UserId { get; set; }
    }
}

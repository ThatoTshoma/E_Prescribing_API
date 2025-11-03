using E_Prescribing_API.Data.Services;
using System.ComponentModel.DataAnnotations;

namespace E_Prescribing_API.Models
{
    public class Surgeon
    {
        [Key]
        public int SurgeonId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; }
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }
        [Display(Name = "Registration Number")]
        public string RegistrationNumber { get; set; }
        public ApplicationUser User { get; set; }
        public int UserId { get; set; }
    }
}

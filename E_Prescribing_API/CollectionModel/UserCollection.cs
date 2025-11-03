using E_Prescribing_API.Data.Services;
using E_Prescribing_API.Models;

namespace E_Prescribing_API.CollectionModel
{
    public class UserCollection
    {
        public ApplicationUser ApplicationUsers { get; set; }
        public Nurse Nurse { get; set; }
        public Pharmacist Pharmacist { get; set; }
        public Surgeon Surgeon { get; set; }
        public Anaesthesiologist Anaesthesiologist { get; set; }
        public string ReturnUrl { get; set; }
    }
}

using E_Prescribing_API.Data.Services;
using E_Prescribing_API.Models;

namespace E_Prescribing_API.CollectionModel
{
    public class RegisterCollection
    {
        public ApplicationUser ApplicationUser { get; set; }
        public MedicalSaff MedicalStaff { get; set; }
        public string Role { get; set; }
    }
}

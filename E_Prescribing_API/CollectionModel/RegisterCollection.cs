using E_Prescribing_API.Data;
using E_Prescribing_API.Models;

namespace E_Prescribing_API.CollectionModel
{
    public class RegisterCollection
    {
        public ApplicationUser ApplicationUser { get; set; }
        public MedicalStaff MedicalStaff { get; set; }
        public string Role { get; set; }
    }
}

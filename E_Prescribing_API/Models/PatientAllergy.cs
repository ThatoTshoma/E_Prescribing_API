using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_Prescribing_API.Models
{
    public class PatientAllergy
    {
        [Key]
        public int AllergyId { get; set; }
        public Patient Patient { get; set; }
        public int PatientId { get; set; }
        public ActiveIngredient ActiveIngredient { get; set; }
        public int ActiveIngredientId { get; set; }
        
    }
}

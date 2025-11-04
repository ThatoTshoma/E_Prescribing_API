using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_Prescribing_API.Models
{
    public class MedicationIngredient
    {
        [Key]
        public int MedicationIngredientId { get; set; }
        public int ActiveIngredientId { get; set; }
        public int MedicationId { get; set; }
        public string ActiveIngredientStrength { get; set; }

    }
}
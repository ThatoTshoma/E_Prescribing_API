using System.ComponentModel.DataAnnotations;

namespace E_Prescribing_API.Models
{
    public class ContraIndication
    {
        [Key]
        public int ContraIndicationId { get; set; }
        public int ActiveIngredientId { get; set; }
        public int ConditionDiagnosisId { get; set; }
    }
}

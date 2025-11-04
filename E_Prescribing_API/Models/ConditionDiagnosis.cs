using System.ComponentModel.DataAnnotations;

namespace E_Prescribing_API.Models
{
    public class ConditionDiagnosis
    {
        [Key]
        public int ConditionId { get; set; }
        public string Icd10Code { get; set; }
        public string Name { get; set; }
    }
}
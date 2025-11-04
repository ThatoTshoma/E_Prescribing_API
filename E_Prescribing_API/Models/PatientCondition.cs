using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_Prescribing_API.Models
{
    public class PatientCondition
    {
        [Key]
        public int PatientConditionId { get; set; }
        public Patient Patient { get; set; }
        public int PatientId { get; set; }
        public ConditionDiagnosis Condition { get; set; }
        public int ConditionId { get; set; }

        [NotMapped]
        public List<int> SelectedCondition { get; set; }
    }
}
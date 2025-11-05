using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_Prescribing_API.Models
{
    public class PatientMedication
    {
        [Key]
        public int PatientMedicationId { get; set; }
        public Patient Patient { get; set; }
        public int PatientId { get; set; }
        public Medication Medication { get; set; }
        public int MedicationId { get; set; }
    }
}

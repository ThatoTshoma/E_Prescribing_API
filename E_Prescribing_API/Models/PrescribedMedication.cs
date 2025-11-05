using System.ComponentModel.DataAnnotations;

namespace E_Prescribing_API.Models
{
    public class PrescribedMedication
    {
        [Key]
        public int PrescribedMedicationId { get; set; }
        public int MedicationId { get; set; }
        public int PrescriptionId { get; set; }
        public int Quantity { get; set; }
        public string Instruction { get; set; }
    }
}

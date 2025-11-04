using System.ComponentModel.DataAnnotations;

namespace E_Prescribing_API.Models
{
    public class Medication
    {
        [Key]
        public int MedicationId { get; set; }
        public string Name { get; set; }
        public int Schedule { get; set; }
        public int DosageFormId { get; set; }
    }
}

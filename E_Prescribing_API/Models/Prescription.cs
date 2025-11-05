using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Prescribing_API.Models
{
    public class Prescription
    {
        [Key]
        public int PrescriptionId { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public bool IsUrgent { get; set; }
        public string Note { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public int PrescriberId { get; set; }
        public MedicalStaff Prescriber { get; set; }
        public int? DispenserId { get; set; }
        public MedicalStaff Dispenser { get; set; }
    }


}

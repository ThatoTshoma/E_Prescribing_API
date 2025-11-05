using E_Prescribing_API.Models;

namespace E_Prescribing_API.CollectionModel
{
    public class PrescriptionCollection
    {
        public Prescription Prescription { get; set; }
        public PrescribedMedication PrescribedMedication { get; set; }
        public Dictionary<int, string> Instructions { get; set; }
        public Dictionary<int, int> Quantities { get; set; }

    }
}

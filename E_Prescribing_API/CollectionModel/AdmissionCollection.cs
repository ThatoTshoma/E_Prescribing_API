using E_Prescribing_API.Models;

namespace E_Prescribing_API.CollectionModel
{
    public class AdmissionCollection
    {
        public PatientAllergy PatientAllergy { get; set; }
        public PatientMedication PatientMedication { get; set; }
        public PatientCondition PatientCondition { get; set; }
        //public PatientBed PatientBed { get; set; }
        //public Booking Booking { get; set; }


        public List<Patient> Patients { get; set; }
        public List<int> SelectedMedication { get; set; }
        public List<int> SelectedCondition { get; set; }
        public List<int> SelectedAllergy { get; set; }


        public int CurrentStep { get; set; }
    }
}

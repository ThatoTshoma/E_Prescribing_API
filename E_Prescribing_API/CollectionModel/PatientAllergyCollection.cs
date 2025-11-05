using E_Prescribing_API.Models;

namespace E_Prescribing_API.CollectionModel
{
    public class PatientAllergyCollection
    {
        public PatientAllergy PatientAllergy { get; set; }
        public List<int> SelectedActiveIngredient { get; set; }
    }
}

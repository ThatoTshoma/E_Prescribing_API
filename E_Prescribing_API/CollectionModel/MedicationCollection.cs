using E_Prescribing_API.Models;

namespace E_Prescribing_API.CollectionModel
{
    public class MedicationCollection
    {
        public MedicationIngredient MedicationIngredient { get; set; }
        public Medication Medication { get; set; }
        public Dictionary<int, string> Strengths { get; set; }
        public List<int> SelectedIngredient { get; set; }


    }
}

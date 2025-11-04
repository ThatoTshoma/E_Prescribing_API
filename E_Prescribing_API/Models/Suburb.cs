using System.ComponentModel.DataAnnotations;

namespace E_Prescribing_API.Models
{
    public class Suburb
    {
        [Key]
        public int SuburbId { get; set; }
        public string Name { get; set; }
        public int PostalCode { get; set; }
        public int CityId { get; set; }
    }
}

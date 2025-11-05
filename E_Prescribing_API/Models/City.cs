using System.ComponentModel.DataAnnotations;

namespace E_Prescribing_API.Models
{
    public class City
    {
        [Key]
        public int CityId { get; set; }
        public string Name { get; set; }    
        public Province province { get; set; }
        public int ProvinceId { get; set; }
    }
}

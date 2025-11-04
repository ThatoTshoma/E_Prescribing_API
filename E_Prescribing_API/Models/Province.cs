using System.ComponentModel.DataAnnotations;

namespace E_Prescribing_API.Models
{
    public class Province
    {
        [Key]
        public int ProvinceId { get; set; }
        public string Name { get; set; }
    }
}

namespace E_Prescribing_API.Models
{
    public class Facility
    {
        public int FacilityId { get; set; }
        public string Name { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string ContactNumber { get; set; }
        public Suburb Suburb { get; set; }
        public int SuburbId { get; set; }
        public FacilityType FacilityType { get; set; }
        public int FacilityTypeId { get; set; }

    }
}

namespace LawFirmTemplate.Data.Entities
{
    public class Firm : BaseEntity
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
        public string Mail { get; set; }
        public string ImageUrl { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string Social1 { get; set; }
        public string Social2 { get; set; }
        public string Social3 { get; set; }
        public string Social4 { get; set; }
        public string Social5 { get; set; }
    }
}

namespace LawFirmTemplate.Data.Entities
{
    public class Contact : BaseEntity
    {
        public string Name { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }
        public string Subject { get; set; }
        public string Area { get; set; }
        public string Message { get; set; }
    }
}

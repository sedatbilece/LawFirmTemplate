using LawFirmTemplate.Data.Enums;

namespace LawFirmTemplate.Data.Entities
{
    public class User : BaseEntity
    {

        //system
        public string UserName { get; set; }
        public string Password { get; set; }
        public RoleType RoleType { get; set; }
        public Status Status { get; set; }
        public string ImageUrl { get; set; }

        //public
        public string DisplayName { get; set; }
        public string Title { get; set; }
        public string PracticeArea { get; set; }
        public string PhoneNumber { get; set; }
        public string Mail { get; set; }
        public string Social1 { get; set; }
        public string Social2 { get; set; }
        public string Social3 { get; set; }

        public int Order { get; set; }

    }
}

using LawFirmTemplate.Data.Entities;

namespace LawFirmTemplate.Models.Home
{
    public class HomeIndexModel
    {

        public List<ClientSays> clientSays { get; set; }
        public List<Data.Entities.User> teams { get; set; }
        public Firm firm { get; set; }


    }
}

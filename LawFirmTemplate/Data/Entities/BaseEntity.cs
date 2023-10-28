using Microsoft.AspNetCore.Http.HttpResults;

namespace LawFirmTemplate.Data.Entities
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            CreatedAt = DateTime.Now;
        }

        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }
    
}

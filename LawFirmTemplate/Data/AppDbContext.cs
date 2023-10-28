using LawFirmTemplate.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LawFirmTemplate.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }

    }
}

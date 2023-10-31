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
        public DbSet<ClientSays> ClientSays { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Firm> Firms { get; set; }
        public DbSet<PracticeArea> PracticeAreas { get; set; }

    }
}

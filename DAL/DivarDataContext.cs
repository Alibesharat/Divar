using divar.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace divar.DAL
{
    public class DivarDataContext : DbContext
    {

        public DbSet<Reservation> Reservations { get; set; }

        public DivarDataContext()
        {
            
        }
        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
          options.UseSqlite($"Data Source=SharifExpert.db");
        }

        public void Initialize()
        {
            // Apply any pending migrations and ensure the database is up to date
            Database.EnsureCreated();
        }
    }
}
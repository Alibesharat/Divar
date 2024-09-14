using divar.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace divar.DAL
{
    public class DivarDataContext : DbContext
    {
        public DbSet<Reservation> Reservations { get; set; }
        public string DbPath { get; }

        public DivarDataContext()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            DbPath = Path.Join(path, "Divar.db");
        }
        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}
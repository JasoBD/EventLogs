using EventLogs.Models;
using Microsoft.EntityFrameworkCore;

namespace EventLogs.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<EventLog> EventLogs { get; set; }
    }

}

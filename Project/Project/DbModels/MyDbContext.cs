using Microsoft.EntityFrameworkCore;

namespace Project.DbModels
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Pair> Pairs { get; set; }
        public DbSet<Device> Devices { get; set; }
    }
}
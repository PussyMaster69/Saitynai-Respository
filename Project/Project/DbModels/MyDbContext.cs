using Microsoft.EntityFrameworkCore;

namespace Project.DbModels
{
    public class MyDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Pair> Pairs { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<ConnectionHistory> ConnectionHistories { get; set; }
        public DbSet<ActiveDevice> ActiveDevices { get; set; }
        public DbSet<Scanner> Scanners { get; set; }
        
        public MyDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
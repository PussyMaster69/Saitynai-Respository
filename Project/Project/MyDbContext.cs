using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project.DbModels;

namespace Project
{
    public class MyDbContext : IdentityDbContext<IdentityUser>
    {   
        public DbSet<PairTable> Pairs { get; set; }
        public DbSet<DeviceTable> Devices { get; set; }
        public DbSet<ConnectionHistoryTable> ConnectionHistories { get; set; }
        public DbSet<ActiveDeviceTable> ActiveDevices { get; set; }
        public DbSet<ScannerTable> Scanners { get; set; }
        
        public MyDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
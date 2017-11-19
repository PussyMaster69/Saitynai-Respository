using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project.DbModels;

namespace Project
{
    public class MyDbContext : IdentityDbContext<IdentityUser>
    {   
        public DbSet<PairTableModel> Pairs { get; set; }
        public DbSet<DeviceTableModel> Devices { get; set; }
        public DbSet<ConnectionHistoryTableModel> ConnectionHistories { get; set; }
        public DbSet<ActiveDeviceTableModel> ActiveDevices { get; set; }
        public DbSet<ScannerTableModel> Scanners { get; set; }
        
        public MyDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Project.DbModels
{
    public class MyDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<SysUser> SysUsers { get; set; }
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
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Project.Models;

namespace Project.DbModels
{
    public class PairTable
    {
        public int Id { get; set; }
        public string FriendlyName { get; set; }
        
        public IdentityUser User { get; set; }
        public DeviceTable Device { get; set; }
        public List<ConnectionHistoryTable> ConnectionHistories { get; set; }
        public List<ActiveDeviceTable> ActiveDevices { get; set; }
    }
}
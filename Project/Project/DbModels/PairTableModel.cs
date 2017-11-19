using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Project.Models;

namespace Project.DbModels
{
    public class PairTableModel
    {
        public int Id { get; set; }
        public string FriendlyName { get; set; }
        
        public IdentityUser User { get; set; }
        public Device Device { get; set; }
        public List<ConnectionHistoryTableModel> ConnectionHistories { get; set; }
        public List<ActiveDeviceTableModel> ActiveDevices { get; set; }
    }
}
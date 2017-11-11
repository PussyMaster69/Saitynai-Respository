using System.Collections.Generic;
using Project.Models;

namespace Project.DbModels
{
    public class Pair
    {
        public int Id { get; set; }
        public string FriendlyName { get; set; }
        
        public SysUser SysUser { get; set; }
        public Device Device { get; set; }
        public List<ConnectionHistory> ConnectionHistories { get; set; }
        public List<ActiveDevice> ActiveDevices { get; set; }
    }
}
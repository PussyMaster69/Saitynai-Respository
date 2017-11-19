using System;

namespace Project.DbModels
{
    public class ActiveDeviceTable
    {
        public int Id { get; set; }
        public DateTime Datetime { get; set; }
        
        public PairTable Pair { get; set; }
    }
}
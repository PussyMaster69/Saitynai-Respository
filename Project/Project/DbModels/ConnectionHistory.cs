using System;

namespace Project.DbModels
{
    public class ConnectionHistory
    {
        public int Id { get; set; }
        public DateTime Datetime { get; set; }
        
        public Pair Pair { get; set; }
    }
}
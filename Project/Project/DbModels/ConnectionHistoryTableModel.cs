using System;

namespace Project.DbModels
{
    public class ConnectionHistoryTableModel
    {
        public int Id { get; set; }
        public DateTime Datetime { get; set; }
        
        public PairTableModel Pair { get; set; }
    }
}
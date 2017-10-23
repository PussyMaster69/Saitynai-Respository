using System;
using System.ComponentModel.DataAnnotations;

namespace Project.DbModels
{
    public class Scanner
    {
        public int Id { get; set; }
        public int State { get; set; }
        public DateTime Datetime { get; set; }
        
        public User User { get; set; }
    }
}
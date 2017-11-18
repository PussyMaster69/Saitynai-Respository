using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.DbModels
{
    public class Device
    {
        [Key]
        public string Address { get; set; }
        public string Name { get; set; }
        
        public List<Pair> Pairs { get; set; }
    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.DbModels
{
    public class DeviceTable
    {
        [Key]
        public string Address { get; set; }
        public string Name { get; set; }
    }
}
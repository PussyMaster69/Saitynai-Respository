using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Protocols;

namespace Project.Models
{
    public class Scanner
    {
        public static int Active => 1;
        public static int Inactive => 0;

        public int Id { get; set; }
        public string Ip { get; set; }
        public int State { get; set; }
        public DateTime Datetime { get; set; }
    }
}

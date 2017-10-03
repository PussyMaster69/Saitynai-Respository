using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models
{
    public class Register : Account
    {
        public string Password { get; set; }
        public string PasswordCheck { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Project.DbModels
{
    public class ScannerTable
    {
        public int Id { get; set; }
        public string Ip { get; set; }
        public DateTime Datetime { get; set; }
        public IdentityUser User { get; set; }
    }
}
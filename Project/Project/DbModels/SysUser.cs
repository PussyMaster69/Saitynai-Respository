using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;

namespace Project.DbModels
{
    public class SysUser
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public int AccessLevel { get; set; }
        public DateTime Datetime { get; set; }
        
        public Pair Pair { get; set; }
        public Scanner Scanner { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace Project.DbModels
{
    public class ActiveDevice
    {
        public int Id { get; set; }
        public DateTime Datetime { get; set; }
        
        public Pair Pair { get; set; }
    }
}
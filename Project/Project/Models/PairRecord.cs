using System;

namespace Project.Models
{
    public class PairRecord
    {
        public int Id { get; set; }
        public DateTime ActivationTime { get; set; }
        public int PairId { get; set; }
        public int ScannerId { get; set; }
    }
}
using Project.Models;

namespace Project.DbModels
{
    public class Pair
    {
        public int Id { get; set; }
        public string FriendlyName { get; set; }
        public Device Device { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace table_stack.Controllers
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }
        public string customerName { get; set; }
        public string phoneNumber { get; set; }
        public int partySize { get; set; }
        public int waitTime { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace table_stack.Controllers
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }
        public int userId { get; set; }

        public int partySize { get; set; }
        public string dateReserved { get; set; }
    }
}
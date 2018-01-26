using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace table_stack.Controllers
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string userType { get; set; }
        public string restaurantName { get; set; }
        public string yelpId { get; set; }
        public string fullName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string phoneNumber { get; set; }
        public string zipCode { get; set; }

        public List<Reservation> userReservations { get; set; }
    }
}
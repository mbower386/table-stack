using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace table_stack.Controllers
{
    [Route ("api/users")]
    public class UsersController : Controller
    {
        public int id = 0;
        private readonly TableStackContext _context;

        public UsersController (TableStackContext context)
        {
            _context = context;

            if (_context.Reservations.Count () == 0)
            {
                _context.Reservations.Add (new Reservation () { customerName = "Matthew Bower", phoneNumber = "1234567890", partySize = 4, waitTime = 20 });

                _context.Reservations.Add (new Reservation () { customerName = "Charles Bower", phoneNumber = "1234567890", partySize = 4, waitTime = 25 });

                _context.Reservations.Add (new Reservation () { customerName = "Elon Musk", phoneNumber = "1234567890", partySize = 1, waitTime = 5 });

                _context.Reservations.Add (new Reservation () { customerName = "Jeff Bezos", phoneNumber = "1234567890", partySize = 2, waitTime = 20 });

                _context.Reservations.Add (new Reservation () { customerName = "Tim Cook", phoneNumber = "1234567890", partySize = 5, waitTime = 25 });

                _context.Reservations.Add (new Reservation () { customerName = "Jack Bauer", phoneNumber = "1234567890", partySize = 4, waitTime = 15 });

                _context.Reservations.Add (new Reservation () { customerName = "Chuck Norris", phoneNumber = "1234567890", partySize = 1, waitTime = 5 });

                _context.Reservations.Add (new Reservation () { customerName = "Steven Seagal", phoneNumber = "1234567890", partySize = 2, waitTime = 20 });

                _context.Reservations.Add (new Reservation () { customerName = "John McClane", phoneNumber = "1234567890", partySize = 5, waitTime = 25 });

                _context.Reservations.Add (new Reservation () { customerName = "John Rambo", phoneNumber = "1234567890", partySize = 4, waitTime = 15 });

                _context.Reservations.Add (new Reservation () { customerName = "Arnold Schwarzenegger", phoneNumber = "1234567890", partySize = 1, waitTime = 5 });

                _context.Reservations.Add (new Reservation () { customerName = "Jason Statham", phoneNumber = "1234567890", partySize = 2, waitTime = 20 });

                _context.Reservations.Add (new Reservation () { customerName = "James Bond", phoneNumber = "1234567890", partySize = 5, waitTime = 25 });

                _context.SaveChanges ();
            }

            if (_context.Users.Count () == 0)
            {
                _context.Users.Add (new User () { Id = ++id, userType = "restaurant", restaurantName = "Some Really Great Restaurant", yelpId = "", fullName = "Some Really Great Restaurant", email = "dude@greatfood.com", password = "greatgrub", phoneNumber = "4155551234", zipCode = "81234", userReservations = new List<Reservation> () });
                _context.SaveChanges ();

                foreach (User u in _context.Users)
                {
                    List<Reservation> tempList = new List<Reservation> ();
                    foreach (Reservation r in _context.Reservations)
                    {
                        if (u.Id == 1)
                        {
                            tempList.Add (r);
                        }

                    }

                    u.userReservations = tempList;
                    _context.SaveChanges ();
                }
            }
        }

        // Gets all users
        [HttpGet]
        public List<User> Get ()
        {
            return _context.Users.ToList ();
        }

        // Gets single user
        [HttpGet ("{id}")]
        public User Get (int id)
        {
            foreach (User u in _context.Users.Include (u => u.userReservations))
            {
                if (u.Id == id)
                {
                    return u;
                }
            }

            return null;
        }

        [HttpGet ("login")]
        public int Login (string email, string password)
        {
            foreach (User u in _context.Users)
            {
                if (u.email == email)
                {
                    if (u.password == password)
                    {
                        return u.Id;
                    }
                }
            }

            return -1;
        }

        // Create new user
        [HttpPost]
        public User Post ([FromBody] User u)
        {
            _context.Users.Add (u);
            _context.SaveChanges ();

            return u;
        }

        // Updates existing user
        [HttpPut ("{id}")]
        public User Put (int id, [FromBody] User user)
        {
            foreach (User u in _context.Users)
            {
                if (u.Id == id)
                {
                    _context.Users.Remove (u);
                    _context.SaveChanges ();
                    _context.Users.Add (user);
                    _context.SaveChanges ();

                    return user;
                }
            }

            return null;
        }

        // Deletes user
        [HttpDelete ("{id}")]
        public string Delete (int id)
        {
            foreach (User u in _context.Users)
            {
                if (u.Id == id)
                {
                    _context.Users.Remove (u);
                    _context.SaveChanges ();

                    return "deleted";
                }
            }

            return "user not found";
        }
    }
}
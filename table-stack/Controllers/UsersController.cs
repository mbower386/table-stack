using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace table_stack.Controllers
{
    [Route ("api/users")]
    public class UsersController : Controller
    {
        private readonly TableStackContext _context;

        public UsersController (TableStackContext context)
        {
            _context = context;

            if (_context.Users.Count () == 0)
            {
                _context.Users.Add (new User () { Id = 1, firstName = "Matthew", lastName = "Bower", email = "mbower386@gmail.com", password = "password", phoneNumber = "9493386876", zipCode = "92691" });
                _context.SaveChanges ();
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
            foreach (User u in _context.Users)
            {
                if (u.Id == id)
                {
                    return u;
                }
            }

            return null;
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
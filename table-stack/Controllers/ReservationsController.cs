using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace table_stack.Controllers
{
    [Route ("api/reservations")]
    public class ReservationsController : Controller
    {
        public int id = 0;
        private readonly TableStackContext _context;

        public ReservationsController (TableStackContext context)
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
        }

        // Gets all reservations
        [HttpGet]
        public List<Reservation> Get ()
        {
            return _context.Reservations.ToList ();
        }

        // Gets single reservation
        [HttpGet ("{id}")]
        public Reservation Get (int id)
        {
            foreach (Reservation r in _context.Reservations)
            {
                if (r.Id == id)
                {
                    return r;
                }
            }

            return null;
        }

        [HttpGet ("userReservations")]
        public List<Reservation> Get (string userId)
        {
            List<Reservation> userReservations = new List<Reservation> ();

            return userReservations;
        }

        // Create new reservation
        [HttpPost]
        public Reservation Post ([FromBody] Reservation r)
        {
            _context.Reservations.Add (r);
            _context.SaveChanges ();

            return r;
        }

        // Update existing reservation
        [HttpPut ("{id}")]
        public Reservation Put (int id, [FromBody] Reservation reservation)
        {
            foreach (Reservation r in _context.Reservations)
            {
                if (r.Id == id)
                {
                    _context.Reservations.Remove (r);
                    _context.SaveChanges ();
                    _context.Reservations.Add (reservation);
                    _context.SaveChanges ();

                    return reservation;
                }
            }

            return null;
        }

        // Deletes reservation
        [HttpDelete ("{id}")]
        public string Delete (int id)
        {
            foreach (Reservation r in _context.Reservations)
            {
                if (r.Id == id)
                {
                    _context.Reservations.Remove (r);
                    _context.SaveChanges ();

                    return "deleted";
                }
            }

            return "reservation not found";
        }

        [HttpGet ("sendMessage")]
        public ActionResult SendSms (string smsCode, string phoneNumber, string fullName, string partySize, string waitTime)
        {
            string textMessage = "";

            switch (smsCode)
            {
                case "1":
                    textMessage = "Hi " + fullName + "!  You just reserved a table with us!  You have " + partySize + " in your party, and your wait time is " + waitTime + " minutes.";
                    break;
                case "2":
                    textMessage = "Hi " + fullName + "!  Your table is almost ready!  Please head back to be seated.";
                    break;
                case "3":
                    textMessage = "Hi " + fullName + "!  You just updated your reservation!  You now have " + partySize + " in your party, and your wait time is " + waitTime + " minutes.";
                    break;
                case "4":
                    textMessage = "Hi " + fullName + "!  You just cancelled your reservation!  Sorry to see you go!";
                    break;
                default:
                    break;
            }

            var accoundSid = "";
            var authToken = "";

            TwilioClient.Init (accoundSid, authToken);

            var toPhoneNumber = new PhoneNumber (phoneNumber);
            var fromPhoneNumber = new PhoneNumber ("");

            var message = MessageResource.Create (
                to: toPhoneNumber,
                from: fromPhoneNumber,
                body: textMessage);

            return Content (message.Sid);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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
                _context.Reservations.Add (new Reservation () { userId = 1, partySize = 4, dateReserved = "Thu Jan 18 2018 17:00:00 GMT-0700 (PDT)", waitTime = 25 });

                _context.Reservations.Add (new Reservation () { userId = 3, partySize = 3, dateReserved = "Wed Jan 17 2018 12:00:00 GMT-0700 (PDT)", waitTime = 20 });

                _context.Reservations.Add (new Reservation () { userId = 2, partySize = 2, dateReserved = "Tues Jan 16 2018 09:00:00 GMT-0700 (PDT)", waitTime = 15 });

                _context.Reservations.Add (new Reservation () { userId = 3, partySize = 4, dateReserved = "Sun Jan 21 2018 12:05:00 GMT-0700 (PDT)", waitTime = 25 });

                _context.Reservations.Add (new Reservation () { userId = 4, partySize = 3, dateReserved = "Mon Jan 22 2018 12:00:00 GMT-0700 (PDT)", waitTime = 20 });

                _context.Reservations.Add (new Reservation () { userId = 1, partySize = 2, dateReserved = "Tues Jan 16 2018 09:00:00 GMT-0700 (PDT)", waitTime = 10 });

                _context.Reservations.Add (new Reservation () { userId = 1, partySize = 4, dateReserved = "Thu Jan 18 2018 17:00:00 GMT-0700 (PDT)", waitTime = 25 });

                _context.Reservations.Add (new Reservation () { userId = 3, partySize = 3, dateReserved = "Wed Jan 17 2018 12:00:00 GMT-0700 (PDT)", waitTime = 20 });

                _context.Reservations.Add (new Reservation () { userId = 2, partySize = 2, dateReserved = "Tues Jan 16 2018 09:00:00 GMT-0700 (PDT)", waitTime = 15 });

                _context.Reservations.Add (new Reservation () { userId = 3, partySize = 4, dateReserved = "Sun Jan 21 2018 12:05:00 GMT-0700 (PDT)", waitTime = 25 });

                _context.Reservations.Add (new Reservation () { userId = 4, partySize = 3, dateReserved = "Mon Jan 22 2018 12:00:00 GMT-0700 (PDT)", waitTime = 20 });

                _context.Reservations.Add (new Reservation () { userId = 1, partySize = 2, dateReserved = "Tues Jan 16 2018 09:00:00 GMT-0700 (PDT)", waitTime = 10 });

                _context.Reservations.Add (new Reservation () {  userId = 1, partySize = 4, dateReserved = "Thu Jan 18 2018 17:00:00 GMT-0700 (PDT)", waitTime = 25 });

                _context.Reservations.Add (new Reservation () { userId = 3, partySize = 3, dateReserved = "Wed Jan 17 2018 12:00:00 GMT-0700 (PDT)", waitTime = 20 });

                _context.Reservations.Add (new Reservation () {  userId = 2, partySize = 2, dateReserved = "Tues Jan 16 2018 09:00:00 GMT-0700 (PDT)", waitTime = 15 });

                _context.Reservations.Add (new Reservation () {  userId = 3, partySize = 4, dateReserved = "Sun Jan 21 2018 12:05:00 GMT-0700 (PDT)", waitTime = 25 });

                _context.Reservations.Add (new Reservation () {  userId = 4, partySize = 3, dateReserved = "Mon Jan 22 2018 12:00:00 GMT-0700 (PDT)", waitTime = 20 });

                _context.Reservations.Add (new Reservation () {  userId = 1, partySize = 2, dateReserved = "Tues Jan 16 2018 09:00:00 GMT-0700 (PDT)", waitTime = 10 });

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
    }
}
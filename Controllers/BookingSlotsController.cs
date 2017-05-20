using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using GMMT.Models;


namespace GMMT.Controllers
{
    [RoutePrefix("api/BookingSlots")]
    public class BookingSlotsController : ApiController
    {
        private TurfDbContext db = new TurfDbContext();

        // GET: api/BookingSlots
        public IQueryable<BookingSlots> GetBookingSlots()
        {
            return db.BookingSlots;
        }


        // GET: api/BookingSlots
        public IQueryable<BookingSlots> GetBookingSlots(DateTime fromDate, DateTime toDate)
        {
            //list<TurfDetails> turfs = new list<TurfDetails>();
            // List<TurfDetails> turfs = new List<TurfDetails>();
            IQueryable<TurfDetails> turfs = null;
            using (var context = new TurfDbContext())
            {
                turfs = from b in context.TurfDetails
                        select b;
            }
            List<TurfDetails> lstTurfDetails = new List<TurfDetails>();
            List<TurfDetails> lstRequiredTurfDetails = new List<TurfDetails>();


            if (turfs.Count() > 0)
            {
                lstTurfDetails = turfs.ToList();
            }

            //lstRequiredTurfDetails= lstTurfDetails.Where(x=>x.AvailableFrom)
            return db.BookingSlots;
        }

        // GET: api/BookingSlots/5
        [ResponseType(typeof(BookingSlots))]
        public IHttpActionResult GetBookingSlots(int id)
        {
            BookingSlots bookingSlots = db.BookingSlots.Find(id);
            if (bookingSlots == null)
            {
                return NotFound();
            }

            return Ok(bookingSlots);
        }

        // PUT: api/BookingSlots/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBookingSlots(int id, BookingSlots bookingSlots)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bookingSlots.Id)
            {
                return BadRequest();
            }

            db.Entry(bookingSlots).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingSlotsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/BookingSlots
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("~/BookingSlot")]
        public IHttpActionResult PostBookingSlots(BookingSlots bookingSlots)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool isDetails = false;
            using (var context = new TurfDbContext())
            {
                // Query to extract user info 
                //isDetails = context.UserDetails
                //               .Any(b => b.MobileNo.ToLower().Trim() == bookingSlots.UserMobileNo.ToLower().Trim() && b.Token.ToString().Trim() == bookingSlots.Token.ToString());

                isDetails = context.UserDetails
                               .Any(b => b.MobileNo.ToLower().Trim() == bookingSlots.UserMobileNo.ToLower().Trim());
                var pastBookings = context.BookingSlots.ToList();

                if (!isDetails)
                {
                    return BadRequest("User details not found");
                }
                else if (pastBookings.Any(x => (x.BookingSlotFrom == bookingSlots.BookingSlotFrom && x.BookingSlotTo == bookingSlots.BookingSlotTo) && x.TurfCode == bookingSlots.TurfCode))
                {
                    return BadRequest("So Sorry! slot booked please choose another slot");
                }
            }

            db.BookingSlots.Add(bookingSlots);
            db.SaveChanges();

            //return CreatedAtRoute("DefaultApi", new { id = bookingSlots.Id }, bookingSlots);
            return Ok();
        }

        // DELETE: api/BookingSlots/5
        [ResponseType(typeof(BookingSlots))]
        public IHttpActionResult DeleteBookingSlots(int id)
        {
            BookingSlots bookingSlots = db.BookingSlots.Find(id);
            if (bookingSlots == null)
            {
                return NotFound();
            }

            db.BookingSlots.Remove(bookingSlots);
            db.SaveChanges();

            return Ok(bookingSlots);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BookingSlotsExists(int id)
        {
            return db.BookingSlots.Count(e => e.Id == id) > 0;
        }

        [HttpGet]
        [Route("~/PastBookings/{userMobileNo}")]
        public IHttpActionResult PastBookings(string userMobileNo)
        {
            using (var context = new TurfDbContext())
            {
                bool flag = context.BookingSlots.Any(x => x.UserMobileNo == userMobileNo);

                if (flag)
                {
                    var pastbookings = context.BookingSlots.Where(x => x.BookingSlotTo < DateTime.UtcNow).ToList();
                    return Ok(pastbookings);
                }
                else
                {
                    return BadRequest("Mobile Number not found");
                }


            }
        }

    }
}
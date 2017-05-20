using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GMMT.Models;

namespace GMMT.Controllers
{
    public class TestBookingSlots1Controller : Controller
    {
        private TurfDbContext db = new TurfDbContext();

        // GET: TestBookingSlots1
        public ActionResult Index()
        {
            return View(db.BookingSlots.ToList());
        }

        // GET: TestBookingSlots1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookingSlots bookingSlots = db.BookingSlots.Find(id);
            if (bookingSlots == null)
            {
                return HttpNotFound();
            }
            return View(bookingSlots);
        }

        // GET: TestBookingSlots1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TestBookingSlots1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TurfCode,UserEmailId,FullName,BookingSlotFrom,BookingSlotTo,InvoiceNo,BookingDate,ChargesPaid")] BookingSlots bookingSlots)
        {
            if (ModelState.IsValid)
            {
                db.BookingSlots.Add(bookingSlots);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bookingSlots);
        }

        // GET: TestBookingSlots1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookingSlots bookingSlots = db.BookingSlots.Find(id);
            if (bookingSlots == null)
            {
                return HttpNotFound();
            }
            return View(bookingSlots);
        }

        // POST: TestBookingSlots1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TurfCode,UserEmailId,FullName,BookingSlotFrom,BookingSlotTo,InvoiceNo,BookingDate,ChargesPaid")] BookingSlots bookingSlots)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookingSlots).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bookingSlots);
        }

        // GET: TestBookingSlots1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookingSlots bookingSlots = db.BookingSlots.Find(id);
            if (bookingSlots == null)
            {
                return HttpNotFound();
            }
            return View(bookingSlots);
        }

        // POST: TestBookingSlots1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BookingSlots bookingSlots = db.BookingSlots.Find(id);
            db.BookingSlots.Remove(bookingSlots);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

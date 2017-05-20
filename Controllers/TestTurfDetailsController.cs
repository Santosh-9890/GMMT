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
    public class TestTurfDetailsController : Controller
    {
        private TurfDbContext db = new TurfDbContext();

        // GET: TestTurfDetails
        public ActionResult Index()
        {
            return View(db.TurfDetails.ToList());
        }

        // GET: TestTurfDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TurfDetails turfDetails = db.TurfDetails.Find(id);
            if (turfDetails == null)
            {
                return HttpNotFound();
            }
            return View(turfDetails);
        }

        // GET: TestTurfDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TestTurfDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TurfId,TurfCode,TurfName,TurfEmailId,Address,Synopsis,MobileNo,PhoneNo,Dimensions,AvailableFrom,AvailableTill,DayCharges,NightCharges")] TurfDetails turfDetails)
        {
            if (ModelState.IsValid)
            {
                db.TurfDetails.Add(turfDetails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(turfDetails);
        }

        // GET: TestTurfDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TurfDetails turfDetails = db.TurfDetails.Find(id);
            if (turfDetails == null)
            {
                return HttpNotFound();
            }
            return View(turfDetails);
        }

        // POST: TestTurfDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TurfId,TurfCode,TurfName,TurfEmailId,Address,Synopsis,MobileNo,PhoneNo,Dimensions,AvailableFrom,AvailableTill,DayCharges,NightCharges")] TurfDetails turfDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(turfDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(turfDetails);
        }

        // GET: TestTurfDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TurfDetails turfDetails = db.TurfDetails.Find(id);
            if (turfDetails == null)
            {
                return HttpNotFound();
            }
            return View(turfDetails);
        }

        // POST: TestTurfDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TurfDetails turfDetails = db.TurfDetails.Find(id);
            db.TurfDetails.Remove(turfDetails);
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

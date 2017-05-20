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
    public class TestUserDetailsController : Controller
    {
        private TurfDbContext db = new TurfDbContext();

        // GET: TestUserDetails
        public ActionResult Index()
        {
            return View(db.UserDetails.ToList());
        }

        // GET: TestUserDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDetails userDetails = db.UserDetails.Find(id);
            if (userDetails == null)
            {
                return HttpNotFound();
            }
            return View(userDetails);
        }

        // GET: TestUserDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TestUserDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,EmailId,FullName,MobileNo,Password,isReadyForOffers")] UserDetails userDetails)
        {
            if (ModelState.IsValid)
            {

                UserDetails details = null;
                string EmailId = userDetails.EmailId;
                // Query to extract user info 
                using (var context = new TurfDbContext())
                {
                    // Query to extarct user info 
                    details = context.UserDetails
                                   .Where(b => b.EmailId.ToLower().Trim() == EmailId.ToLower().Trim())
                                   .FirstOrDefault();

                    if (details != null)
                    {
                        return View("User details already exists");
                    }

                    userDetails.CreatedDate = DateTime.UtcNow;
                    userDetails.LastLoginDate = DateTime.UtcNow;
                    //userDetails.Token = Guid.NewGuid();
                    db.UserDetails.Add(userDetails);
                    db.SaveChanges();

                    details = context.UserDetails
                                  .Where(b => b.EmailId.ToLower().Trim() == EmailId.ToLower().Trim())
                                  .FirstOrDefault();

                    if (details == null)
                    {
                        return View("User details not found");
                    }
                }

            }
            return View("index", db.UserDetails.ToList());
        }

        // GET: TestUserDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDetails userDetails = db.UserDetails.Find(id);
            if (userDetails == null)
            {
                return HttpNotFound();
            }
            return View(userDetails);
        }

        // POST: TestUserDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,EmailId,FullName,MobileNo,Password,isReadyForOffers")] UserDetails userDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userDetails);
        }

        // GET: TestUserDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDetails userDetails = db.UserDetails.Find(id);
            if (userDetails == null)
            {
                return HttpNotFound();
            }
            return View(userDetails);
        }

        // POST: TestUserDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserDetails userDetails = db.UserDetails.Find(id);
            db.UserDetails.Remove(userDetails);
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

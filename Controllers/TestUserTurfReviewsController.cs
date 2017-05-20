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
    public class TestUserTurfReviewsController : Controller
    {
        private TurfDbContext db = new TurfDbContext();

        // GET: TestUserTurfReviews
        public ActionResult Index()
        {
            return View(db.UserTurfReviews.ToList());
        }

        // GET: TestUserTurfReviews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserTurfReview userTurfReview = db.UserTurfReviews.Find(id);
            if (userTurfReview == null)
            {
                return HttpNotFound();
            }
            return View(userTurfReview);
        }

        // GET: TestUserTurfReviews/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TestUserTurfReviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TurfCode,UserEmailId,UserReview")] UserTurfReview userTurfReview)
        {
            if (ModelState.IsValid)
            {
                db.UserTurfReviews.Add(userTurfReview);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userTurfReview);
        }

        // GET: TestUserTurfReviews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserTurfReview userTurfReview = db.UserTurfReviews.Find(id);
            if (userTurfReview == null)
            {
                return HttpNotFound();
            }
            return View(userTurfReview);
        }

        // POST: TestUserTurfReviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TurfCode,UserEmailId,UserReview")] UserTurfReview userTurfReview)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userTurfReview).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userTurfReview);
        }

        // GET: TestUserTurfReviews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserTurfReview userTurfReview = db.UserTurfReviews.Find(id);
            if (userTurfReview == null)
            {
                return HttpNotFound();
            }
            return View(userTurfReview);
        }

        // POST: TestUserTurfReviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserTurfReview userTurfReview = db.UserTurfReviews.Find(id);
            db.UserTurfReviews.Remove(userTurfReview);
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GMMT.Models;
using System.Web.Mvc;

namespace GMMT.Controllers
{
    [System.Web.Http.RoutePrefix("api/UserDetails")]

    public class UserDetailsController : ApiController
    {
        private TurfDbContext db = new TurfDbContext();

        [System.Web.Http.Route("~/GetUserDetails")]
        public IHttpActionResult GetUserDetails()
        {
            // var json = JsonConvert.SerializeObject(db.UserDetails);
            return Ok(db.UserDetails);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //********** Sign Up *************
        [System.Web.Http.HttpPost]
        [ValidateAntiForgeryToken]
        [System.Web.Http.Route("~/SignUp")]
        public IHttpActionResult SignUp([Bind(Include = "EmailId,FullName,MobileNo,Password,isReadyForOffers")] UserDetails userDetails)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserDetailsViewModel objUserDetailsViewModel = new UserDetailsViewModel();
                    UserDetails details = null;
                    string MobileNo = userDetails.MobileNo;
                    // Query to extract user info 
                    using (var context = new TurfDbContext())
                    {
                        // Query to extarct user info 
                        details = context.UserDetails
                                       .Where(b => b.MobileNo.ToLower().Trim() == MobileNo.ToLower().Trim())
                                       .FirstOrDefault();

                        if (details != null)
                        {
                            return BadRequest("User details already exists");
                        }

                        userDetails.CreatedDate = DateTime.UtcNow;
                        userDetails.LastLoginDate = DateTime.UtcNow;
                        //userDetails.Token = Guid.NewGuid();
                        db.UserDetails.Add(userDetails);
                        db.SaveChanges();

                        details = context.UserDetails
                                      .Where(b => b.MobileNo.ToLower().Trim() == MobileNo.ToLower().Trim())
                                      .FirstOrDefault();

                        if (details == null)
                        {
                            return BadRequest("User details not found");
                        }
                        else
                        {
                            objUserDetailsViewModel.EmailId = details.EmailId;
                            objUserDetailsViewModel.FullName = details.FullName;
                            objUserDetailsViewModel.MobileNo = details.MobileNo;
                            //objUserDetailsViewModel.Token = details.Token;
                        }
                    }
                    return Ok(objUserDetailsViewModel);
                }
                string messages = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
                return BadRequest("Please check your request model" + " " + messages);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //********** Sign In  *************


        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("~/SignIn")]
        public IHttpActionResult SignIn([Bind(Include = "EmailId,FullName,MobileNo,Token,Password")] UserDetailsViewModel userDetails)
        {
            try
            {
                UserDetailsViewModel objUserDetailsViewModel = new UserDetailsViewModel();
                UserDetails details = null;

                using (var context = new TurfDbContext())
                {
                                        // Query to extract user info 
                    details = context.UserDetails
                                   .Where(b => b.MobileNo.Trim().ToLower() == userDetails.MobileNo.Trim().ToLower())
                                   .FirstOrDefault();

                    if (details == null)
                    {
                        return NotFound();
                    }
                    else if (details.Password == userDetails.Password)
                    {
                        objUserDetailsViewModel.EmailId = details.EmailId;
                        objUserDetailsViewModel.FullName = details.FullName;
                        objUserDetailsViewModel.MobileNo = details.MobileNo;
                      //  objUserDetailsViewModel.Token = details.Token;
                        return Ok(objUserDetailsViewModel);
                    }
                    else
                    {
                        return BadRequest("Password Mismatch");
                    }
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //[System.Web.Http.HttpGet]
        //[System.Web.Http.Route("~/SignIn/{emailId}/{password}")]
        //public IHttpActionResult SignIn(string emailId, string password)
        //{
        //    try
        //    {
        //        UserDetailsViewModel objUserDetailsViewModel = new UserDetailsViewModel();
        //        UserDetails details = null;

        //        using (var context = new TurfDbContext())
        //        {

        //            // Query to extract user info 
        //            details = context.UserDetails
        //                           .Where(b => b.EmailId.Trim().ToLower() == emailId.Trim().ToLower())
        //                           .FirstOrDefault();

        //            if (details == null)
        //            {
        //                return NotFound();
        //            }
        //            else if (details.Password == password)
        //            {
        //                objUserDetailsViewModel.EmailId = details.EmailId;
        //                objUserDetailsViewModel.FullName = details.FullName;
        //                objUserDetailsViewModel.MobileNo = details.MobileNo;
        //                objUserDetailsViewModel.Token = details.Token;
        //                return Ok(objUserDetailsViewModel);
        //            }
        //            else
        //            {
        //                return BadRequest("Password Mismatch");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //}

        //********** get turf list *************
        //[System.Web.Http.HttpGet]
        //[System.Web.Http.Route("~/GetTurfList/{fromDate}/{toDate}")]
        //public IHttpActionResult GetTurfList(DateTime fromDate, DateTime toDate)
        //{

        //    try
        //    {
        //        List<BookingSlots> bookingDetails = new List<BookingSlots>();
        //        List<TurfDetails> turfDetails = new List<TurfDetails>();


        //        using (var context = new TurfDbContext())
        //        {
        //            // Query for the bookingDetails && turfDetails
        //            bookingDetails = context.BookingSlots.Where(x => x.BookingSlotFrom == fromDate && x.BookingSlotTo <= toDate).ToList();
        //            turfDetails = context.TurfDetails.ToList();
        //        }

        //       // var jsonTurfDetails = JsonConvert.SerializeObject(turfDetails);

        //        if (bookingDetails.Count == 0)
        //        {
        //            return Ok(turfDetails);
        //        }
        //        else
        //        {
        //            foreach (var item in bookingDetails)
        //            {
        //                var itemToRemove = turfDetails.Single(r => r.TurfCode == item.TurfCode);
        //                turfDetails.Remove(itemToRemove);
        //            }
        //           // jsonTurfDetails = JsonConvert.SerializeObject(turfDetails);
        //        }
        //        return Ok(turfDetails);
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }

        //}

        //********** For user turf review *************
        //[System.Web.Http.HttpGet]
        //[System.Web.Http.Route("~/UserTurfReview/{emailId}/{turfCode}/{review}")]
        //public IHttpActionResult UserTurfReview(string emailId, string turfCode, string review)
        //{
        //    try
        //    {
        //        bool isUserExist = false;
        //        using (var context = new TurfDbContext())
        //        {
        //            isUserExist = context.UserTurfReviews.Any(x => x.UserEmailId == emailId && x.TurfCode == turfCode);

        //            if (isUserExist)
        //            {
        //                return BadRequest("User already Exist");
        //            }
        //            else
        //            {
        //                UserTurfReview objUserTurfReview = new UserTurfReview();
        //                objUserTurfReview.TurfCode = turfCode;
        //                objUserTurfReview.UserEmailId = emailId;
        //                objUserTurfReview.UserReview = review;
        //                db.UserTurfReviews.Add(objUserTurfReview);
        //                db.SaveChanges();

        //                return Ok();
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //}


        //********** To book the slots *************

        //[System.Web.Http.HttpPost]
        //[ValidateAntiForgeryToken]
        //[System.Web.Http.Route("~/BookingSlot")]
        //public IHttpActionResult PostBookingSlots(BookingSlots bookingSlots)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.BookingSlots.Add(bookingSlots);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = bookingSlots.Id }, bookingSlots);
        //}

    }
}
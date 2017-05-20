using GMMT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GMMT.Controllers
{
    [RoutePrefix("api/UserTurfReview")]
    public class UserTurfReviewController : ApiController
    {
        private TurfDbContext db = new TurfDbContext();

        [HttpGet]
        [Route("~/UserTurfReview/{mobileNo}/{turfCode}/{review}")]
        public IHttpActionResult UserTurfReview(string mobileNo, string turfCode, string review)
        {
            try
            {
                bool isUserExist = false;
                using (var context = new TurfDbContext())
                {
                    isUserExist = context.UserTurfReviews.Any(x => x.MobileNo == mobileNo && x.TurfCode == turfCode);

                    if (isUserExist)
                    {
                        return BadRequest("Great! you have already shared your opinion");
                    }
                    else
                    {
                        UserTurfReview objUserTurfReview = new UserTurfReview();
                        objUserTurfReview.TurfCode = turfCode;
                        objUserTurfReview.MobileNo = mobileNo;
                        objUserTurfReview.UserReview = review;
                        db.UserTurfReviews.Add(objUserTurfReview);
                        db.SaveChanges();

                        return Ok();
                    }
                }

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("~/AverageOfRating")]
        public IHttpActionResult AverageOfRating()
        {
            try
            {
                using (var context = new TurfDbContext())
                {
                    var query = from item in context.UserTurfReviews
                                group item by new { TurfCode = item.TurfCode } into grouped
                                select new
                                {
                                    TurfCode = grouped.Key.TurfCode,
                                    AverageValue = grouped.Average(x => x.UserRating)
                                };

                    List<UserRatingViewModel> lstUserRatingViewModel = new List<UserRatingViewModel>();

                    foreach (var item in query)
                    {
                        UserRatingViewModel userRating = new UserRatingViewModel();
                        userRating.TurfCode = item.TurfCode;
                        userRating.UserRating = item.AverageValue;
                        lstUserRatingViewModel.Add(userRating);
                    }

                    return Ok(lstUserRatingViewModel);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

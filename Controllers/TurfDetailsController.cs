using GMMT.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using static GMMT.Models.EnumRegion;

namespace GMMT.Controllers
{
    public class TurfDetailsController : ApiController
    {

        //********** get turf list *************
        [HttpGet]
        [Route("~/GetTurfList/{fromDate}/{toDate}/{regionCode}")]
        public IHttpActionResult GetTurfList([FromUri]string fromDate, [FromUri]string toDate, string regionCode)
        {

            try
            {
                List<BookingSlots> bookingDetails = new List<BookingSlots>();
                List<TurfDetails> turfDetails = new List<TurfDetails>();
                //DateTime FromDate = Convert.ToDateTime(fromDate);
                //DateTime ToDate = Convert.ToDateTime(toDate);

                string format = "yyyyMMddHHmmssfff";
                //                string dateTime = "20140123205803252";
                DateTime FromDate = DateTime.ParseExact(fromDate, format, CultureInfo.InvariantCulture);
                DateTime ToDate = DateTime.ParseExact(toDate, format, CultureInfo.InvariantCulture);

                //  DateTime.ParseExact(toDate, format, CultureInfo.InvariantCulture);

                if (ToDate > FromDate)
                {
                    using (var context = new TurfDbContext())
                    {
                        // Query for the bookingDetails && turfDetails
                        bookingDetails = context.BookingSlots.Where(x => (x.BookingSlotFrom >= FromDate && x.BookingSlotTo <= ToDate) && x.RegionCode == regionCode).ToList();
                        turfDetails = context.TurfDetails.Where(x => x.RegionCode == regionCode).ToList();
                        //bookingDetails = context.BookingSlots.Where(x => FromDate >= x.BookingSlotFrom && ToDate <= x.BookingSlotTo).ToList();
                        //for (int i = 0; i < bookingDetails.Count; i++)
                        //{
                        //    if (turfDetails.Any(x=>x.TurfCode.ToLower()==bookingDetails[i].TurfCode))
                        //    {
                        //        turfDetails = turfDetails.Remove(turfDetails.Where(x=>x.TurfCode.ToLower()== bookingDetails[i].TurfCode));
                        //        turfDetails = context.TurfDetails.ToList();

                        //    }

                        //}

                        //foreach (var item in bookingDetails)
                        //{
                        //    if (turfDetails.Any(x => x.TurfCode.ToLower() == item.TurfCode.ToLower()))
                        //    {
                        //        var turfItem = turfDetails.Single(x => x.TurfCode.ToLower() == item.TurfCode.ToLower());
                        //        turfDetails.Remove(turfItem);
                        //    }
                        //}
                    }
                }
                else
                {
                    return BadRequest("Enter valid time");
                }

                // var jsonTurfDetails = JsonConvert.SerializeObject(turfDetails);
                // var jsonTurfDetails = turfDetails;
                if (bookingDetails.Count == 0)
                {
                    return Ok(turfDetails);
                }
                else
                {
                    foreach (var item in bookingDetails)
                    {
                        var itemToRemove = turfDetails.Single(r => r.TurfCode == item.TurfCode);
                        turfDetails.Remove(itemToRemove);
                    }
                    // jsonTurfDetails = JsonConvert.SerializeObject(turfDetails);
                }
                return Ok(turfDetails);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        //**** get server time *******
        [HttpGet]
        [Route("~/GetServerTime")]
        public string GetServerTime()
        {
            try
            {
                DateTime utcTime = DateTime.UtcNow;
                TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi);
                return localTime.ToString("yyyyMMddhh");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("~/GetRegion")]
        public IHttpActionResult GetRegion()
        {
            try
            {
                //  IEnumerable<Region> values = Enum.GetValues(typeof(Region)).Cast<Region>();

                var list = Enum.GetValues(typeof(Region))
    .Cast<Region>()
    .Select(v => v.ToString())
    .ToList();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}

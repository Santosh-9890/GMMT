using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GMMT.Models
{
    public class UserTurfReview
    {
        [Key]
        public int Id { get; set; }
        public string TurfCode { get; set; }
        [EmailAddress]
        public string UserEmailId { get; set; }
        public string MobileNo { get; set; }
        public string UserReview { get; set; }
        public int UserRating { get; set; }
    }

    [NotMapped]
    public class UserRatingViewModel
    {
        public string TurfCode { get; set; }
        public double UserRating { get; set; }
    }


}
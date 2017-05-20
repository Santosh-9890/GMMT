using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GMMT.Models
{
    public class UserDetails
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "EmailId cannot be empty")]
        [EmailAddress]

        public string EmailId { get; set; }
        [Required(ErrorMessage = "FullName cannot be empty")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "MobileNo cannot be empty")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        [DataType(DataType.PhoneNumber)]
        public string MobileNo { get; set; }
        [Required(ErrorMessage = "Password cannot be empty")]

        public string Password { get; set; }
        public bool isReadyForOffers { get; set; }
        // public Guid Token { get; set; }
        public DateTime LastLoginDate { get; set; }
        public int ExpiryDays { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
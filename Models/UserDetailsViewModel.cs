using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GMMT.Models
{
    [NotMapped]
    public class UserDetailsViewModel
    {
        public string EmailId { get; set; }
        public string FullName { get; set; }
        public string MobileNo { get; set; }
        public Guid Token { get; set; }
        public string Password { get; set; }
    }
}
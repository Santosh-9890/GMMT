using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GMMT.Models
{
    public class TurfImages
    {
        [Key]
        public int Id { get; set; }
        public int TurfId { get; set; }
        public string ImageUrl { get; set; }
    }
}
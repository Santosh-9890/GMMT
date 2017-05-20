using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace GMMT.Models
{
    public class TurfDbContext : DbContext
    {
        public DbSet<TurfDetails> TurfDetails { get; set; }
        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<BookingSlots> BookingSlots { get; set; }
        public DbSet<UserTurfReview> UserTurfReviews { get; set; }
        public DbSet<TurfImages> TurfImages { get; set; }

    }
}
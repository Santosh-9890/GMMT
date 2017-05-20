namespace GMMT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BookingSlots", "UserMobileNo", c => c.String());
            AddColumn("dbo.UserTurfReviews", "MobileNo", c => c.String());
            AddColumn("dbo.UserTurfReviews", "UserRating", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserTurfReviews", "UserRating");
            DropColumn("dbo.UserTurfReviews", "MobileNo");
            DropColumn("dbo.BookingSlots", "UserMobileNo");
        }
    }
}

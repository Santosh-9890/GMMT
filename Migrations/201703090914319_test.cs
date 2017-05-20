namespace GMMT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookingSlots",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TurfCode = c.String(),
                        UserEmailId = c.String(),
                        FullName = c.String(),
                        BookingSlotFrom = c.DateTime(nullable: false),
                        BookingSlotTo = c.DateTime(nullable: false),
                        InvoiceNo = c.String(),
                        BookingDate = c.DateTime(nullable: false),
                        ChargesPaid = c.Int(nullable: false),
                        Token = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TurfDetails",
                c => new
                    {
                        TurfId = c.Int(nullable: false, identity: true),
                        TurfCode = c.String(),
                        TurfName = c.String(),
                        TurfEmailId = c.String(),
                        Address = c.String(),
                        Synopsis = c.String(),
                        MobileNo = c.String(),
                        PhoneNo = c.String(),
                        Dimensions = c.String(),
                        AvailableFrom = c.DateTime(nullable: false),
                        AvailableTill = c.DateTime(nullable: false),
                        DayCharges = c.Int(nullable: false),
                        NightCharges = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TurfId);
            
            CreateTable(
                "dbo.UserDetails",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        EmailId = c.String(nullable: false),
                        FullName = c.String(nullable: false),
                        MobileNo = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        isReadyForOffers = c.Boolean(nullable: false),
                        Token = c.Guid(nullable: false),
                        LastLoginDate = c.DateTime(nullable: false),
                        ExpiryDays = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.UserTurfReviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TurfCode = c.String(),
                        UserEmailId = c.String(),
                        UserReview = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserTurfReviews");
            DropTable("dbo.UserDetails");
            DropTable("dbo.TurfDetails");
            DropTable("dbo.BookingSlots");
        }
    }
}

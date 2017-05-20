namespace GMMT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class regioncode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BookingSlots", "RegionCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BookingSlots", "RegionCode");
        }
    }
}

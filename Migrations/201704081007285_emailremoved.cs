namespace GMMT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class emailremoved : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.BookingSlots", "UserEmailId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BookingSlots", "UserEmailId", c => c.String());
        }
    }
}

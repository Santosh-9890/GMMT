namespace GMMT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tokenremoved : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.BookingSlots", "Token");
            DropColumn("dbo.UserDetails", "Token");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserDetails", "Token", c => c.Guid(nullable: false));
            AddColumn("dbo.BookingSlots", "Token", c => c.Guid(nullable: false));
        }
    }
}

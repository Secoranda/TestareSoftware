namespace siteControl.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Trucks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DispatchExt = c.Int(nullable: false),
                        TruckID = c.Int(nullable: false),
                        CurrentLocation = c.String(nullable: false),
                        CurrentStatus = c.String(nullable: false),
                        Date_Posted = c.String(),
                        Time_Posted = c.String(),
                        Date_Edited = c.String(),
                        Time_Edited = c.String(),
                        Comments = c.String(),
                        UserID_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.UserID_ID)
                .Index(t => t.UserID_ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Name = c.String(),
                        Country = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trucks", "UserID_ID", "dbo.Users");
            DropIndex("dbo.Trucks", new[] { "UserID_ID" });
            DropTable("dbo.Users");
            DropTable("dbo.Trucks");
        }
    }
}

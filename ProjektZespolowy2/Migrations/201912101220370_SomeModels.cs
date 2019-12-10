namespace ProjektZespolowy2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SomeModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProjektZespolowy_Browser",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Profile_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ProjektZespolowy_Profile", t => t.Profile_ID)
                .Index(t => t.Profile_ID);
            
            CreateTable(
                "dbo.ProjektZespolowy_MAC",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MACAdress = c.String(),
                        Profile_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ProjektZespolowy_Profile", t => t.Profile_ID)
                .Index(t => t.Profile_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjektZespolowy_MAC", "Profile_ID", "dbo.ProjektZespolowy_Profile");
            DropForeignKey("dbo.ProjektZespolowy_Browser", "Profile_ID", "dbo.ProjektZespolowy_Profile");
            DropIndex("dbo.ProjektZespolowy_MAC", new[] { "Profile_ID" });
            DropIndex("dbo.ProjektZespolowy_Browser", new[] { "Profile_ID" });
            DropTable("dbo.ProjektZespolowy_MAC");
            DropTable("dbo.ProjektZespolowy_Browser");
        }
    }
}

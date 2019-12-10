namespace ProjektZespolowy2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProjektZespolowy_Roles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.ProjektZespolowy_UserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.ProjektZespolowy_Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.ProjektZespolowy_Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.ProjektZespolowy_Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.ProjektZespolowy_UserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProjektZespolowy_Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ProjektZespolowy_UserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.ProjektZespolowy_Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ProjektZespolowy_Profile",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        isAuthenticated = c.Boolean(nullable: false),
                        Code = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjektZespolowy_UserRoles", "UserId", "dbo.ProjektZespolowy_Users");
            DropForeignKey("dbo.ProjektZespolowy_UserLogins", "UserId", "dbo.ProjektZespolowy_Users");
            DropForeignKey("dbo.ProjektZespolowy_UserClaims", "UserId", "dbo.ProjektZespolowy_Users");
            DropForeignKey("dbo.ProjektZespolowy_UserRoles", "RoleId", "dbo.ProjektZespolowy_Roles");
            DropIndex("dbo.ProjektZespolowy_UserLogins", new[] { "UserId" });
            DropIndex("dbo.ProjektZespolowy_UserClaims", new[] { "UserId" });
            DropIndex("dbo.ProjektZespolowy_Users", "UserNameIndex");
            DropIndex("dbo.ProjektZespolowy_UserRoles", new[] { "RoleId" });
            DropIndex("dbo.ProjektZespolowy_UserRoles", new[] { "UserId" });
            DropIndex("dbo.ProjektZespolowy_Roles", "RoleNameIndex");
            DropTable("dbo.ProjektZespolowy_Profile");
            DropTable("dbo.ProjektZespolowy_UserLogins");
            DropTable("dbo.ProjektZespolowy_UserClaims");
            DropTable("dbo.ProjektZespolowy_Users");
            DropTable("dbo.ProjektZespolowy_UserRoles");
            DropTable("dbo.ProjektZespolowy_Roles");
        }
    }
}

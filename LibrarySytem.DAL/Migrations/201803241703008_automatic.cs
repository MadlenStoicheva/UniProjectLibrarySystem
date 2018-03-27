namespace LibrarySytem.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class automatic : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImgURL = c.String(),
                        NumberISBN = c.String(),
                        Title = c.String(),
                        Author = c.String(),
                        PublishingHouse = c.String(),
                        ReleaseDate = c.DateTime(nullable: false),
                        Availability = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TakeABooks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BookId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        DateTaken = c.DateTime(nullable: false),
                        DateForReturn = c.DateTime(nullable: false),
                        DateReturn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.BookId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImgURL = c.String(),
                        Username = c.String(),
                        Password = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        IsAdmin = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TakeABooks", "UserId", "dbo.Users");
            DropForeignKey("dbo.TakeABooks", "BookId", "dbo.Books");
            DropIndex("dbo.TakeABooks", new[] { "UserId" });
            DropIndex("dbo.TakeABooks", new[] { "BookId" });
            DropTable("dbo.Users");
            DropTable("dbo.TakeABooks");
            DropTable("dbo.Books");
        }
    }
}

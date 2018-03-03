namespace Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addimage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "imgURL", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "imgURL");
        }
    }
}

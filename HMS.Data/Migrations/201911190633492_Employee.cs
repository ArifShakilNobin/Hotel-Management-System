namespace HMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Employee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BlogTypes", "Address", c => c.String());
            AddColumn("dbo.BlogTypes", "Email", c => c.String());
            AddColumn("dbo.BlogTypes", "Mobile", c => c.String());
            DropColumn("dbo.BlogTypes", "Title");
            DropColumn("dbo.BlogTypes", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BlogTypes", "Description", c => c.String());
            AddColumn("dbo.BlogTypes", "Title", c => c.String());
            DropColumn("dbo.BlogTypes", "Mobile");
            DropColumn("dbo.BlogTypes", "Email");
            DropColumn("dbo.BlogTypes", "Address");
        }
    }
}

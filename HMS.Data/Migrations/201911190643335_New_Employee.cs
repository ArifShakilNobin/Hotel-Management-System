namespace HMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class New_Employee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AccomodationTypes", "Address", c => c.String());
            AddColumn("dbo.AccomodationTypes", "Email", c => c.String());
            AddColumn("dbo.AccomodationTypes", "Mobile", c => c.String());
            DropColumn("dbo.AccomodationTypes", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AccomodationTypes", "Description", c => c.String());
            DropColumn("dbo.AccomodationTypes", "Mobile");
            DropColumn("dbo.AccomodationTypes", "Email");
            DropColumn("dbo.AccomodationTypes", "Address");
        }
    }
}

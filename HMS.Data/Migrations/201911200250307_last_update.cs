namespace HMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class last_update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AccomodationTypes", "Description", c => c.String());
            DropColumn("dbo.AccomodationTypes", "Address");
            DropColumn("dbo.AccomodationTypes", "Email");
            DropColumn("dbo.AccomodationTypes", "Mobile");
            DropTable("dbo.BlogTypes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.BlogTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        Email = c.String(),
                        Mobile = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.AccomodationTypes", "Mobile", c => c.String());
            AddColumn("dbo.AccomodationTypes", "Email", c => c.String());
            AddColumn("dbo.AccomodationTypes", "Address", c => c.String());
            DropColumn("dbo.AccomodationTypes", "Description");
        }
    }
}

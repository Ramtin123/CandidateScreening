namespace CandidateScreening.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addresslineChange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Addresses", "Line1", c => c.String());
            AddColumn("dbo.Addresses", "Line2", c => c.String());
            DropColumn("dbo.Addresses", "Address1");
            DropColumn("dbo.Addresses", "Address2");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Addresses", "Address2", c => c.String());
            AddColumn("dbo.Addresses", "Address1", c => c.String());
            DropColumn("dbo.Addresses", "Line2");
            DropColumn("dbo.Addresses", "Line1");
        }
    }
}

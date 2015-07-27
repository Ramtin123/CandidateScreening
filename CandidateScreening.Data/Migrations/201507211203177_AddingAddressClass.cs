namespace CandidateScreening.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingAddressClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StreetNumber = c.String(),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        PostCode = c.String(),
                        Suburb = c.String(),
                        Country = c.String(),
                        DefaultAddress = c.Boolean(nullable: false),
                        AddressType = c.Int(nullable: false),
                        PatientId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Patients", t => t.PatientId)
                .Index(t => t.PatientId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Addresses", "PatientId", "dbo.Patients");
            DropIndex("dbo.Addresses", new[] { "PatientId" });
            DropTable("dbo.Addresses");
        }
    }
}

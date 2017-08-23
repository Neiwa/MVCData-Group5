namespace MVCData_Group5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShortenPhoneNo : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "PhoneNo", c => c.String(nullable: false, maxLength: 30));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "PhoneNo", c => c.String(nullable: false, maxLength: 250));
        }
    }
}

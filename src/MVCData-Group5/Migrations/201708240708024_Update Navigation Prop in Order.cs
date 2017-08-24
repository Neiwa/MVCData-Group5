namespace MVCData_Group5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateNavigationPropinOrder : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Orders", "Customer");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "Customer", c => c.Int(nullable: false));
        }
    }
}

namespace CashMachineApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class thirdMigration : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Documents", "DocumentStatus");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Documents", "DocumentStatus", c => c.Int(nullable: false));
        }
    }
}

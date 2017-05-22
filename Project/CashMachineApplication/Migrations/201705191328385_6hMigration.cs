namespace CashMachineApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _6hMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductStores",
                c => new
                    {
                        ProductId = c.Int(nullable: false),
                        TotalCount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductStores", "ProductId", "dbo.Products");
            DropIndex("dbo.ProductStores", new[] { "ProductId" });
            DropTable("dbo.ProductStores");
        }
    }
}

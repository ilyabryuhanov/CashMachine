namespace CashMachineApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FourthMigration : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ProductOrders", new[] { "Product_ProductId" });
            RenameColumn(table: "dbo.ProductOrders", name: "Product_ProductId", newName: "ProductId");
            DropPrimaryKey("dbo.ProductOrders");
            AlterColumn("dbo.ProductOrders", "ProductId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.ProductOrders", "ProductId");
            CreateIndex("dbo.ProductOrders", "ProductId");
            DropColumn("dbo.ProductOrders", "ProductOrderId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductOrders", "ProductOrderId", c => c.Long(nullable: false, identity: true));
            DropIndex("dbo.ProductOrders", new[] { "ProductId" });
            DropPrimaryKey("dbo.ProductOrders");
            AlterColumn("dbo.ProductOrders", "ProductId", c => c.Int());
            AddPrimaryKey("dbo.ProductOrders", "ProductOrderId");
            RenameColumn(table: "dbo.ProductOrders", name: "ProductId", newName: "Product_ProductId");
            CreateIndex("dbo.ProductOrders", "Product_ProductId");
        }
    }
}

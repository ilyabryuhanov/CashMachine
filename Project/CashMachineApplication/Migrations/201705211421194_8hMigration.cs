namespace CashMachineApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _8hMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductOrderDocuments", "ProductOrder_ProductId", "dbo.ProductOrders");
            DropForeignKey("dbo.ProductOrderDocuments", "Document_DocumentId", "dbo.Documents");
            DropIndex("dbo.ProductOrders", new[] { "ProductId" });
            DropIndex("dbo.ProductOrderDocuments", new[] { "ProductOrder_ProductId" });
            DropIndex("dbo.ProductOrderDocuments", new[] { "Document_DocumentId" });
            RenameColumn(table: "dbo.ProductOrders", name: "ProductId", newName: "Product_ProductId");
            DropPrimaryKey("dbo.ProductOrders");
            AddColumn("dbo.ProductOrders", "ProductOrderId", c => c.Long(nullable: false, identity: true));
            AddColumn("dbo.ProductOrders", "Document_DocumentId", c => c.Long());
            AlterColumn("dbo.ProductOrders", "Product_ProductId", c => c.Int());
            AddPrimaryKey("dbo.ProductOrders", "ProductOrderId");
            CreateIndex("dbo.ProductOrders", "Document_DocumentId");
            CreateIndex("dbo.ProductOrders", "Product_ProductId");
            AddForeignKey("dbo.ProductOrders", "Document_DocumentId", "dbo.Documents", "DocumentId");
            DropTable("dbo.ProductOrderDocuments");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProductOrderDocuments",
                c => new
                    {
                        ProductOrder_ProductId = c.Int(nullable: false),
                        Document_DocumentId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductOrder_ProductId, t.Document_DocumentId });
            
            DropForeignKey("dbo.ProductOrders", "Document_DocumentId", "dbo.Documents");
            DropIndex("dbo.ProductOrders", new[] { "Product_ProductId" });
            DropIndex("dbo.ProductOrders", new[] { "Document_DocumentId" });
            DropPrimaryKey("dbo.ProductOrders");
            AlterColumn("dbo.ProductOrders", "Product_ProductId", c => c.Int(nullable: false));
            DropColumn("dbo.ProductOrders", "Document_DocumentId");
            DropColumn("dbo.ProductOrders", "ProductOrderId");
            AddPrimaryKey("dbo.ProductOrders", "ProductId");
            RenameColumn(table: "dbo.ProductOrders", name: "Product_ProductId", newName: "ProductId");
            CreateIndex("dbo.ProductOrderDocuments", "Document_DocumentId");
            CreateIndex("dbo.ProductOrderDocuments", "ProductOrder_ProductId");
            CreateIndex("dbo.ProductOrders", "ProductId");
            AddForeignKey("dbo.ProductOrderDocuments", "Document_DocumentId", "dbo.Documents", "DocumentId", cascadeDelete: true);
            AddForeignKey("dbo.ProductOrderDocuments", "ProductOrder_ProductId", "dbo.ProductOrders", "ProductId", cascadeDelete: true);
        }
    }
}

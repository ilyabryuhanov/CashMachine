namespace CashMachineApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _5hMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductOrders", "Document_DocumentId", "dbo.Documents");
            DropIndex("dbo.ProductOrders", new[] { "Document_DocumentId" });
            CreateTable(
                "dbo.ProductOrderDocuments",
                c => new
                    {
                        ProductOrder_ProductId = c.Int(nullable: false),
                        Document_DocumentId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductOrder_ProductId, t.Document_DocumentId })
                .ForeignKey("dbo.ProductOrders", t => t.ProductOrder_ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Documents", t => t.Document_DocumentId, cascadeDelete: true)
                .Index(t => t.ProductOrder_ProductId)
                .Index(t => t.Document_DocumentId);
            
            DropColumn("dbo.ProductOrders", "Document_DocumentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductOrders", "Document_DocumentId", c => c.Long());
            DropForeignKey("dbo.ProductOrderDocuments", "Document_DocumentId", "dbo.Documents");
            DropForeignKey("dbo.ProductOrderDocuments", "ProductOrder_ProductId", "dbo.ProductOrders");
            DropIndex("dbo.ProductOrderDocuments", new[] { "Document_DocumentId" });
            DropIndex("dbo.ProductOrderDocuments", new[] { "ProductOrder_ProductId" });
            DropTable("dbo.ProductOrderDocuments");
            CreateIndex("dbo.ProductOrders", "Document_DocumentId");
            AddForeignKey("dbo.ProductOrders", "Document_DocumentId", "dbo.Documents", "DocumentId");
        }
    }
}

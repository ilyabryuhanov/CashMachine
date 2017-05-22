namespace CashMachineApplication.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class firstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        DocumentId = c.Long(nullable: false, identity: true),
                        DocumentStatus = c.Int(nullable: false),
                        DocumentType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DocumentId);
            
            CreateTable(
                "dbo.ProductOrders",
                c => new
                    {
                        ProductOrderId = c.Long(nullable: false, identity: true),
                        Count = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Product_ProductId = c.Int(),
                        Document_DocumentId = c.Long(),
                    })
                .PrimaryKey(t => t.ProductOrderId)
                .ForeignKey("dbo.Products", t => t.Product_ProductId)
                .ForeignKey("dbo.Documents", t => t.Document_DocumentId)
                .Index(t => t.Product_ProductId)
                .Index(t => t.Document_DocumentId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductOrders", "Document_DocumentId", "dbo.Documents");
            DropForeignKey("dbo.ProductOrders", "Product_ProductId", "dbo.Products");
            DropIndex("dbo.ProductOrders", new[] { "Document_DocumentId" });
            DropIndex("dbo.ProductOrders", new[] { "Product_ProductId" });
            DropTable("dbo.Products");
            DropTable("dbo.ProductOrders");
            DropTable("dbo.Documents");
        }
    }
}

namespace Infrastructure.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class duong : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CouponItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateExpired = c.DateTime(nullable: false),
                        IsUsed = c.Boolean(nullable: false),
                        UserPackageId = c.Int(nullable: false),
                        OrderId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Order", t => t.OrderId)
                .ForeignKey("dbo.UserCouponPackage", t => t.UserPackageId, cascadeDelete: true)
                .Index(t => t.UserPackageId)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PaymentType = c.Int(nullable: false),
                        Status = c.String(),
                        OrderDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.OrderDetail",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        ProductVariantId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductVariant", t => t.ProductVariantId, cascadeDelete: true)
                .ForeignKey("dbo.Order", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.ProductVariantId);
            
            CreateTable(
                "dbo.ProductVariant",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        Size = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Picture = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 192),
                        FullName = c.String(),
                        Address = c.String(),
                        Phone = c.String(nullable: false, maxLength: 11),
                        Avatar = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Username, unique: true)
                .Index(t => t.Phone, unique: true);
            
            CreateTable(
                "dbo.UserCouponPackage",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CouponPackageId = c.Int(nullable: false),
                        DrinkQuantity = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UserId = c.Int(nullable: false),
                        PurchasedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CouponPackage", t => t.CouponPackageId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.CouponPackageId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.CouponPackage",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DrinkQuantity = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Picture = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserCouponPackage", "UserId", "dbo.User");
            DropForeignKey("dbo.UserCouponPackage", "CouponPackageId", "dbo.CouponPackage");
            DropForeignKey("dbo.CouponItem", "UserPackageId", "dbo.UserCouponPackage");
            DropForeignKey("dbo.Order", "UserId", "dbo.User");
            DropForeignKey("dbo.OrderDetail", "OrderId", "dbo.Order");
            DropForeignKey("dbo.ProductVariant", "ProductId", "dbo.Product");
            DropForeignKey("dbo.OrderDetail", "ProductVariantId", "dbo.ProductVariant");
            DropForeignKey("dbo.CouponItem", "OrderId", "dbo.Order");
            DropIndex("dbo.UserCouponPackage", new[] { "UserId" });
            DropIndex("dbo.UserCouponPackage", new[] { "CouponPackageId" });
            DropIndex("dbo.User", new[] { "Phone" });
            DropIndex("dbo.User", new[] { "Username" });
            DropIndex("dbo.ProductVariant", new[] { "ProductId" });
            DropIndex("dbo.OrderDetail", new[] { "ProductVariantId" });
            DropIndex("dbo.OrderDetail", new[] { "OrderId" });
            DropIndex("dbo.Order", new[] { "UserId" });
            DropIndex("dbo.CouponItem", new[] { "OrderId" });
            DropIndex("dbo.CouponItem", new[] { "UserPackageId" });
            DropTable("dbo.CouponPackage");
            DropTable("dbo.UserCouponPackage");
            DropTable("dbo.User");
            DropTable("dbo.Product");
            DropTable("dbo.ProductVariant");
            DropTable("dbo.OrderDetail");
            DropTable("dbo.Order");
            DropTable("dbo.CouponItem");
        }
    }
}

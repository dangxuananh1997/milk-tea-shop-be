namespace Infrastructure.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class duong_04072018_1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductVariant", "Id", "dbo.OrderDetail");
            DropIndex("dbo.ProductVariant", new[] { "Id" });
            DropPrimaryKey("dbo.OrderDetail");
            DropPrimaryKey("dbo.ProductVariant");
            AlterColumn("dbo.OrderDetail", "Id", c => c.Int(nullable: false));
            AlterColumn("dbo.ProductVariant", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.OrderDetail", "Id");
            AddPrimaryKey("dbo.ProductVariant", "Id");
            CreateIndex("dbo.OrderDetail", "Id");
            AddForeignKey("dbo.OrderDetail", "Id", "dbo.ProductVariant", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetail", "Id", "dbo.ProductVariant");
            DropIndex("dbo.OrderDetail", new[] { "Id" });
            DropPrimaryKey("dbo.ProductVariant");
            DropPrimaryKey("dbo.OrderDetail");
            AlterColumn("dbo.ProductVariant", "Id", c => c.Int(nullable: false));
            AlterColumn("dbo.OrderDetail", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.ProductVariant", "Id");
            AddPrimaryKey("dbo.OrderDetail", "Id");
            CreateIndex("dbo.ProductVariant", "Id");
            AddForeignKey("dbo.ProductVariant", "Id", "dbo.OrderDetail", "Id");
        }
    }
}

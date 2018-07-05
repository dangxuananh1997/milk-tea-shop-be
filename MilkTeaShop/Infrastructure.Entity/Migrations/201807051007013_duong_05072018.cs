namespace Infrastructure.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class duong_05072018 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.User", "Username", c => c.String(nullable: false, maxLength: 192));
            CreateIndex("dbo.User", "Username", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.User", new[] { "Username" });
            AlterColumn("dbo.User", "Username", c => c.String());
        }
    }
}

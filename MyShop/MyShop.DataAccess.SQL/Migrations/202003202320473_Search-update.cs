namespace MyShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Searchupdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "Category", c => c.Guid(nullable: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Category", c => c.String());
        }
    }
}

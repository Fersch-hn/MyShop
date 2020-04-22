namespace MyShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Databaseupdated : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "Category", c => c.Guid());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Category", c => c.Guid(nullable: false));
        }
    }
}

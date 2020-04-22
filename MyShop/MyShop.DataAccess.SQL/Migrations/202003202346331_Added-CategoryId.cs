namespace MyShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCategoryId : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.ProductCategories");
            AlterColumn("dbo.ProductCategories", "Id", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.ProductCategories", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.ProductCategories");
            AlterColumn("dbo.ProductCategories", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.ProductCategories", "Id");
        }
    }
}

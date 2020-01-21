namespace MyShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MySecondUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderItems", "ProductName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderItems", "ProductName");
        }
    }
}

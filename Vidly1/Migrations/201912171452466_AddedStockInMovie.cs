namespace Vidly1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedStockInMovie : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "Stock", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movies", "Stock");
        }
    }
}

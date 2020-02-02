namespace Vidly1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAvailableStockInMovie : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rentals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateRented = c.DateTime(),
                        DateReturned = c.DateTime(),
                        Customer_Id = c.Int(nullable: false),
                        Movie_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.Movie_Id, cascadeDelete: true)
                .Index(t => t.Customer_Id)
                .Index(t => t.Movie_Id);
            
            AddColumn("dbo.Movies", "AvailableStock", c => c.Int(nullable: false));

            Sql("Update Movies SET AvailableStock = Stock");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rentals", "Movie_Id", "dbo.Movies");
            DropForeignKey("dbo.Rentals", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Rentals", new[] { "Movie_Id" });
            DropIndex("dbo.Rentals", new[] { "Customer_Id" });
            DropColumn("dbo.Movies", "AvailableStock");
            DropTable("dbo.Rentals");
        }
    }
}

namespace Vidly1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMembershipTypes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MembershipTypes", "SignUpFee", c => c.Short(nullable: false));
            DropColumn("dbo.MembershipTypes", "SignUpFree");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MembershipTypes", "SignUpFree", c => c.Short(nullable: false));
            DropColumn("dbo.MembershipTypes", "SignUpFee");
        }
    }
}

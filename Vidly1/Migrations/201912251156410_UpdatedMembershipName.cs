namespace Vidly1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedMembershipName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MembershipTypes", "Name", c => c.String(nullable: false, maxLength: 255));
            DropColumn("dbo.MembershipTypes", "MembershipName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MembershipTypes", "MembershipName", c => c.String(nullable: false, maxLength: 255));
            DropColumn("dbo.MembershipTypes", "Name");
        }
    }
}

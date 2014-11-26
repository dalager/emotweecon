namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGuidToTweet : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tweets", "Guid", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tweets", "Guid");
        }
    }
}

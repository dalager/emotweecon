namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addEmotionsToTweets : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tweets", "Emotion", c => c.String());
            AddColumn("dbo.Tweets", "EmotionScore", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tweets", "EmotionScore");
            DropColumn("dbo.Tweets", "Emotion");
        }
    }
}

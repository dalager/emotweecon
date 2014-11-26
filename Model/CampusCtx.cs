namespace Model
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class CampusCtx : DbContext
    {
        // Your context has been configured to use a 'CampusCtx' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Model.CampusCtx' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'CampusCtx' 
        // connection string in the application configuration file.
        public CampusCtx()
            : base("name=DefaultConnection")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

         public virtual DbSet<Tweet> Tweets { get; set; }
    }

    public class Tweet
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string User { get; set; }
        public DateTime Date { get; set; }
        public string Emotion { get; set; }
        public float EmotionScore { get; set; }
        public Guid Guid { get; set; }
    }
}
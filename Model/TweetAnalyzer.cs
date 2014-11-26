using System;
using System.Diagnostics;
using System.Linq;

namespace Model
{
    public class TweetAnalyzer
    {

        public TweetAnalyzer()
        {

        }
        public Tweet ProcessTweetMessage(TweetDto tweet)
        {
            var datacontext = new CampusCtx();
            // Idempotency. We can handle double deliveries
            var existing = datacontext.Tweets.FirstOrDefault(x=>x.Guid==tweet.Guid);
            if (existing != null)
            {
                return existing;
            }

            var sw = Stopwatch.StartNew();
            try
            {
                var sociallyticResponse = Sociallytic.Post(tweet.Content);
                if (sociallyticResponse == null)
                {
                    Console.Out.WriteLine("NO RESPONSE FROM SOCIALLYTIC.DK");
                    return null;
                }
                var message = new Tweet()
                {
                    Guid=tweet.Guid,
                    Date = tweet.Date,
                    Text = tweet.Content,
                    Emotion = sociallyticResponse.sentiment,
                    EmotionScore = sociallyticResponse.sentiment_score_words
                };
                datacontext.Tweets.Add(message);
                datacontext.SaveChanges();

                Console.Out.WriteLine("Response from sociallytic.dk in " + sw.Elapsed.TotalSeconds + " s");
                Console.Out.WriteLine("Done.");
                return message;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Model;

namespace EmoTweeCon.Webjob
{
    public class ServiceBusMessageHandler
    {
        private static readonly TweetAnalyzer Analyzer = new TweetAnalyzer();
        public static void Handle([ServiceBusTrigger("incomingtweet")] TweetDto tweet, TextWriter log)
        {
            log.WriteLine("Got servicebus message. Processing it");
            log.WriteLine("Time in queue " + (DateTime.Now - tweet.Date).TotalSeconds + " seconds");
            var msg = Analyzer.ProcessTweetMessage(tweet);
            
            log.WriteLine("Notifying website..");
            SignalRNotifier.NotifyWebsite(msg);
        }
    }
}
using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.WindowsAzure.Storage;
using Model;
using Newtonsoft.Json;

namespace EmoTweeCon.Webjob
{
    public class StorageQueueMessageHandler
    {
        private static readonly TweetAnalyzer Analyzer = new TweetAnalyzer();

        public static void WriteLog([QueueTrigger("none")] string logMessage,
            DateTimeOffset expirationTime,
            DateTimeOffset insertionTime,
            DateTimeOffset nextVisibleTime,
            string id,
            string popReceipt,
            int dequeueCount,
            string queueTrigger,
            CloudStorageAccount cloudStorageAccount,
            TextWriter logger)
        {
            logger.WriteLine(
                "logMessage={0}\n" +
                "expirationTime={1}\ninsertionTime={2}\n" +
                "nextVisibleTime={3}\n" +
                "id={4}\npopReceipt={5}\ndequeueCount={6}\n" +
                "queue endpoint={7} queueTrigger={8}",
                logMessage, expirationTime,
                insertionTime,
                nextVisibleTime, id,
                popReceipt, dequeueCount,
                cloudStorageAccount.QueueEndpoint,
                queueTrigger);
        }

        public static void HandleSimple([QueueTrigger("incomingtweet")] TweetDto tweet, TextWriter logger)
        {
            try
            {
                logger.WriteLine("Time in queue " + (DateTime.Now - tweet.Date).TotalSeconds + " seconds");
                logger.WriteLine(JsonConvert.SerializeObject(tweet));
                var msg = Analyzer.ProcessTweetMessage(tweet);
                logger.WriteLine("Notifying website..");
                SignalRNotifier.NotifyWebsite(msg);
            }
            catch (Exception e)
            {
                logger.WriteLine("Failed: ");
                logger.WriteLine(e);
                Console.Error.WriteLine(e);
                throw;
            }
        }
    }
}
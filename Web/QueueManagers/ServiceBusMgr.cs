using System;
using Emotweecon.Web.Models;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;
using Model;

namespace Emotweecon.Web.QueueManagers
{
    public class ServiceBusMgr : IQueueMgr
    {
        public void Send(TweetDto tweet)
        {
            // connecting
            string connectionString =
                CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
            var namespaceManager =
                NamespaceManager.CreateFromConnectionString(connectionString);

            // create queueue if not present
            if (!namespaceManager.QueueExists("incomingtweet"))
            {
                namespaceManager.CreateQueue("incomingtweet");
            }

            // creating client
            var client = QueueClient.CreateFromConnectionString(connectionString, "incomingtweet");
            //sending
            client.SendAsync(
                new BrokeredMessage(
                    new TweetDto() { Content = tweet.Content, 
                        Date = DateTime.Now }));
        }
    }
}
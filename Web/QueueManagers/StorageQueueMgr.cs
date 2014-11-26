using System;
using System.Configuration;
using Emotweecon.Web.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Model;
using Newtonsoft.Json;

namespace Emotweecon.Web.QueueManagers
{
    public class StorageQueueMgr : IQueueMgr {
        public void Send(TweetDto tweet)
        {
            var storageAccount = 
                CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["AzureWebJobsStorage"].ToString());
            
            CloudQueueClient queueClient = 
                storageAccount.CreateCloudQueueClient();
            
            var tweetqueue = 
                queueClient.GetQueueReference("incomingtweet");
            tweetqueue.CreateIfNotExists();
          
            var msg = new CloudQueueMessage(JsonConvert.SerializeObject(tweet));

            
            
            tweetqueue.AddMessageAsync(msg);
        }
    }
}
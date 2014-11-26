using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Emotweecon.Web.Models;
using Emotweecon.Web.QueueManagers;
using Model;

namespace Emotweecon.Web.Controllers
{
    public class TweetController : ApiController
    {
        private readonly CampusCtx _datacontext;
        private readonly IQueueMgr _queueManager;
        public TweetController()
        {
            _datacontext = new CampusCtx();
            _queueManager = new StorageQueueMgr(); // ServiceBusMgr eller StorageQueueMgr
        }
        [HttpPost]
        public Tweet Post(TweetInput tweet)
        {
  
            var dto = new TweetDto
            {
                Guid = Guid.NewGuid(),
                Content = tweet.Content,
                Date = DateTime.Now
            };
//            var tweetanalyzer = new TweetAnalyzer();
//            return tweetanalyzer.ProcessTweetMessage(dto);
            _queueManager.Send(dto);
            return null;
        }

        [HttpGet]
        public IEnumerable<TweetViewModel> Get()
        {
            return _datacontext.Tweets
                .OrderByDescending(x => x.Date)
                .Take(10)
                .ToList()
                .Select(x => new TweetViewModel
                {
                    Text = x.Text,
                    Date = x.Date,
                    EmotionScore = x.EmotionScore.ToString()
                });
        }

        [HttpDelete]
        public void Delete()
        {
            _datacontext.Database.ExecuteSqlCommand("delete from Tweets");
        }
    }
}

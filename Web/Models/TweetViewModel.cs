using System;

namespace Emotweecon.Web.Models
{
    public class TweetViewModel
    {
        public string Text { get; set; }
        public string EmotionScore { get; set; }
        public DateTime Date { get; set; }
    }
}
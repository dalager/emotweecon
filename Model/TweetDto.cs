using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Model
{
    public class TweetDto
    {
        public TweetDto()
        {
            
        }

        public string Content { get; set; }
        public DateTime Date { get; set; }
        public Guid Guid { get; set; }
    }

    
}
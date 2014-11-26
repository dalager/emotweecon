using Emotweecon.Web.Models;
using Model;

namespace Emotweecon.Web.QueueManagers
{
    public interface IQueueMgr
    {
        void Send(TweetDto tweet);
    }
}
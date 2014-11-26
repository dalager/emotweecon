using System;
using System.Web.Configuration;
using Microsoft.AspNet.SignalR.Client;
using Model;

namespace EmoTweeCon.Webjob
{
    public class SignalRNotifier
    {
        private static readonly Connection Conn;

        static SignalRNotifier()
        {
            var signalRUrl = WebConfigurationManager.AppSettings["webroot"] + "echo";
            Console.Out.WriteLine("Using " + signalRUrl + " for notifications");
            Conn = new Connection(signalRUrl);
            Conn.Start().Wait();
        }

        public static void NotifyWebsite(Tweet tweet)
        {
            try
            {
                Conn.Send(tweet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
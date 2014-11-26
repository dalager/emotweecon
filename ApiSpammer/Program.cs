using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Emotweecon.Web.Models;
using Newtonsoft.Json;

namespace ApiSpammer
{
    class Program
    {
        static void Main(string[] args)
        {
            int numberOfMeesages;
            if (args.Length == 0 || !int.TryParse(args[0].ToString(),out numberOfMeesages))
            {
                Console.Out.WriteLine("- provide number of messages to spam with");
                return;
            }
            var baseUrl = ConfigurationManager.AppSettings["webroot"];

            Console.Out.WriteLine("Vil sende {0} requests mod api med url angivet i appsettings['webroot']: "+baseUrl);

            var tasks = new List<Task> ();
            for (int i = 0; i < numberOfMeesages; i++)
            {
                var t = SendTweetToAPI();
                tasks.Add(t);
            }

            Console.Out.WriteLine("Waiting for {0} tasks to complete", tasks.Count);
            Task.WaitAll(tasks.ToArray(), TimeSpan.FromSeconds(10));

            Console.Out.WriteLine("Done");
        }

        private static Task SendTweetToAPI()
        {
            var client = new HttpClient();
            client.BaseAddress= new Uri(ConfigurationManager.AppSettings["webroot"]);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var tweet = new TweetInput()
            {
                Content = "Test content "+Guid.NewGuid()
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(tweet),Encoding.UTF8,"application/json");
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/tweet")
            {
                Content = stringContent
            };

            return client.SendAsync(request).ContinueWith(resp =>
            {
                Console.Out.WriteLine(resp.Result.StatusCode);
            });

        }
    }
}

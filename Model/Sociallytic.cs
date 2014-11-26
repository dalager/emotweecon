using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace Model
{
    public class Sociallytic
    {
        public static SociallyticResponse Get(string str)
        {
            var client = new HttpClient();
            str = HttpUtility.UrlEncode(str);
            var resp = client.GetStringAsync(String.Format("http://api.sociallytic.dk/?key=campusdays14&txt={0}", str));
            Task.WaitAll(resp);

            var sociallyticResponse = JsonConvert.DeserializeObject<SociallyticResponse>(resp.Result);

            return sociallyticResponse;
        }

        public static SociallyticResponse Post(string input)
        {
            var client = new HttpClient();

            var formUrlEncodedContent = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("key","chrdal"),
                new KeyValuePair<string, string>("txt",input)
            });
            var resp = client.PostAsync(String.Format("http://api.sociallytic.dk/"), formUrlEncodedContent);
            Task.WaitAll(resp);

            var readAsStringAsync = resp.Result.Content.ReadAsStringAsync();
            Task.WaitAll(readAsStringAsync);
            var sociallyticResponse = JsonConvert.DeserializeObject<SociallyticResponse>(readAsStringAsync.Result);

            return sociallyticResponse;

        }
    }
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace RaceTime.Common.Helpers
{
    public static class ApiWrapper
    {
        public static string ApiUrl = "http://localhost:61544/api/";

        public static void Get<T>(string url)
        {
            T response;

            using (HttpClient  client = new HttpClient())
            {
                // response = JsonConvert.DeserializeObject<T>(client.GetAsync(""));
                var test = client.GetAsync(ApiUrl + url);
                
            }

           // return response;
        }

        //public static T Post<T>(string url, object data)
        //{
        //    WebRequest webRequest = WebRequest.Create(ApiUrl + url);
        //    webRequest.Method = "POST";
        //    webRequest.ContentType = "application/json";

        //    Stream reqStream = null;

        //    reqStream = webRequest.GetRequestStream();
        //    byte[] postArray = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));
        //    reqStream.Write(postArray, 0, postArray.Length);

        //    StreamReader sr = null;

        //    var webResponse = webRequest.GetResponse();
        //    sr = new StreamReader(webResponse.GetResponseStream());
        //    string response = sr.ReadToEnd();

        //    return JsonConvert.DeserializeObject<T>(response);
        //}
    }
}

using Model;
using Nancy.Json;
using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;

namespace QuoteAPI
{
    public class YahooFinanceAPI
    {
        private readonly System.Collections.Specialized.NameValueCollection appSettings = ConfigurationManager.AppSettings;
        private readonly HttpClient Client = new HttpClient();
        private readonly JavaScriptSerializer Serializer = new JavaScriptSerializer();

        public YahooFinanceAPI()
        {
            Client.BaseAddress = new Uri("https://apidojo-yahoo-finance-v1.p.rapidapi.com");
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Client.DefaultRequestHeaders.Add("X-RAPIDAPI-KEY", appSettings["RapidKey"]);
            Client.DefaultRequestHeaders.Add("X-RAPIDAPI-HOST", appSettings["RapidHost"]);
        }

        public float GetCurrentPrice(string symbol)
        {
            HttpResponseMessage response = Client.GetAsync($"/stock/v2/get-summary?symbol={symbol}").Result;

            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                Response content = Serializer.Deserialize<Response>(result);

                float fmt = content.price.regularMarketPrice.fmt;
                return fmt;
            }

            
            return -1F;
        }

    }
}

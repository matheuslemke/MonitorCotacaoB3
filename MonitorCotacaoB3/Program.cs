using Model;
using Nancy.Json;
using StockAPI;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace MonitorCotacaoB3
{
    class Program
    {
        static void Main(string[] args)
        {
            YahooFinanceAPI yahooFinanceAPI = new YahooFinanceAPI();
            yahooFinanceAPI.CheckPrice("PETR4.SA");
        }
    }
}

namespace StockAPI
{
    public class YahooFinanceAPI
    {
        static HttpClient Client = new HttpClient();

        public void CheckPrice(string symbol)
        {
            Client.BaseAddress = new Uri("https://apidojo-yahoo-finance-v1.p.rapidapi.com");
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Client.DefaultRequestHeaders.Add("X-RAPIDAPI-KEY", "22fe836a6bmsh1d796034ff46278p10a23cjsn59c17d10c169");
            Client.DefaultRequestHeaders.Add("X-RAPIDAPI-HOST", "apidojo-yahoo-finance-v1.p.rapidapi.com");

            HttpResponseMessage response = Client.GetAsync($"/stock/v2/get-summary?symbol={symbol}").Result;
            
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var serializer = new JavaScriptSerializer();
                YahooResponse yahooResponse = serializer.Deserialize<YahooResponse>(result);
                Console.WriteLine(yahooResponse.ToString());
            }

        }

    }
}

namespace Model
{
    public class YahooResponse
    {
        public Price price { get; set; }

        public override string ToString()
        {
            return $"{base.ToString()} : Raw: {price.regularMarketPrice.raw} Fmt: {price.regularMarketPrice.fmt}";
        }
    }

    public class Price
    {
        public RegularMarketPrice regularMarketPrice { get; set; }

        public override string ToString()
        {
            return $"{base.ToString()} : Raw: {regularMarketPrice.raw} Fmt: {regularMarketPrice.fmt}";
        }

    }

    public class RegularMarketPrice
    {
        public float raw { get; set; }
        public float fmt { get; set; }

    }


}
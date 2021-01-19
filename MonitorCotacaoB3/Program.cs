using Model;
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
            yahooFinanceAPI.checkPrice("PETR4.SA");
        }
    }
}

namespace StockAPI
{
    public class YahooFinanceAPI
    {
        static HttpClient client = new HttpClient();

        public async void checkPrice(string symbol)
        {
            client.BaseAddress = new Uri("https://apidojo-yahoo-finance-v1.p.rapidapi.com");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("X-RAPIDAPI-KEY", "22fe836a6bmsh1d796034ff46278p10a23cjsn59c17d10c169");
            client.DefaultRequestHeaders.Add("X-RAPIDAPI-HOST", "apidojo-yahoo-finance-v1.p.rapidapi.com");

            HttpResponseMessage response = client.GetAsync($"/stock/v2/get-summary?symbol={symbol}").Result;

            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;

                StockPrice content = Newtonsoft.Json.JsonConvert.DeserializeObject<StockPrice>(result);

                //StockPrice content = await response.Content.ReadAsAsync<StockPrice>();
                Console.WriteLine(content.regularMarketPrice.fmt);
            }

        }

    }
}

namespace Model
{
    public class RegularMarketPrice
    {
        public float raw { get; set; }
        public float fmt { get; set; }

    }

    public class StockPrice
    {        
        public RegularMarketPrice regularMarketPrice { get; set; }

        //public override string ToString()
        //{
        //    return $"{base.ToString()} : Raw: {regularMarketPrice.raw} Fmt: {regularMarketPrice.fmt}";
        //}

    }
}
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
            // Ler entradas
            string symbol = "PETR4.SA";
            float salePrice = 32.67F;
            float purchasePrice = 26.59F;

            // Rodar continuamente
            YahooFinanceAPI api = new YahooFinanceAPI();
            Response response = api.GetSummary(symbol);

            float price = response.price.regularMarketPrice.fmt;

            if (price >= salePrice)
            {
                Console.WriteLine("Vender");
            }
            else if (price <= purchasePrice)
            {
                Console.WriteLine("Comprar");
            }

        }
    }

}

namespace StockAPI
{

    public class YahooFinanceAPI
    {
        private readonly HttpClient Client = new HttpClient();

        public Response GetSummary(string symbol)
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
                Response yahooResponse = serializer.Deserialize<Response>(result);
                return yahooResponse;
            }

            return new Response();
        }

    }
}

namespace Model
{

    public class Response
    {
        public Price price { get; set; }

        public override string ToString()
        {
            return $"price: {price}";
        }
    }

    public class Price
    {
        public RegularMarketPrice regularMarketPrice { get; set; }

        public override string ToString()
        {
            return $"regularMarketPrice: {regularMarketPrice}";
        }

    }

    public class RegularMarketPrice
    {
        public float fmt { get; set; }

        public override string ToString()
        {
            return $"fmt: {fmt}";
        }
    }

}
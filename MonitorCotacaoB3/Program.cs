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
            float purchasePrice = 29.59F;

            // Rodar continuamente
            StockPriceChecker stockPriceChecker = new StockPriceChecker();
            stockPriceChecker.checkPrices(symbol, salePrice, purchasePrice);

        }
    }

    class StockPriceChecker
    {
        private readonly YahooFinanceAPI api = new YahooFinanceAPI();
        private readonly StockRecommend recommend = new StockRecommend();

        public void checkPrices(string symbol, float salePrice, float purchasePrice)
        {
            float price = api.GetCurrentPrice(symbol);

            if (price <= purchasePrice)
            {
                recommend.purchase();
            }
            else if (price >= salePrice)
            {
                recommend.sale();
            }

        }
    }

    class StockRecommend
    {
        public void sale()
        {
            Console.WriteLine("Vender");
        }

        public void purchase()
        {
            Console.WriteLine("Comprar");
        }
    }

}

namespace StockAPI
{

    public class YahooFinanceAPI
    {
        private readonly HttpClient Client = new HttpClient();
        private readonly JavaScriptSerializer Serializer = new JavaScriptSerializer();

        public float GetCurrentPrice(string symbol)
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
                
                Response yahooResponse = Serializer.Deserialize<Response>(result);

                float fmt = yahooResponse.price.regularMarketPrice.fmt;
                return fmt;
            }

            return 0F;
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
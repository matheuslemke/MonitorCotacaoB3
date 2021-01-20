using Model;
using Nancy.Json;
using StockAPI;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Timers;

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

            StockTimer stockTimer = new StockTimer();
            stockTimer.SetTimer(symbol, salePrice, purchasePrice);
        }

    }

    class StockTimer
    {
        private readonly StockPriceChecker stockPriceChecker = new StockPriceChecker();

        public void SetTimer(string symbol, float salePrice, float purchasePrice)
        {

            Timer timer = new Timer(6000);
            
            timer.Elapsed += (sender, e) => {
                Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}", e.SignalTime);
                stockPriceChecker.checkPrices(symbol, salePrice, purchasePrice);
            };
            timer.AutoReset = true;
            timer.Enabled = true;

            Console.WriteLine("\nPress the Enter key to exit the application...\n");
            Console.WriteLine("The application started at {0:HH:mm:ss.fff}", DateTime.Now);
            Console.ReadLine();
            timer.Stop();
            timer.Dispose();

            Console.WriteLine("Terminating the application...");

        }
    }

    class StockPriceChecker
    {
        private readonly YahooFinanceAPI api = new YahooFinanceAPI();
        private readonly StockRecommend recommend = new StockRecommend();

        public void checkPrices(string symbol, float salePrice, float purchasePrice)
        {
            float price = api.GetCurrentPrice(symbol);

            if (price >= 0F)
            {
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

        public YahooFinanceAPI()
        {
            Client.BaseAddress = new Uri("https://apidojo-yahoo-finance-v1.p.rapidapi.com");
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Client.DefaultRequestHeaders.Add("X-RAPIDAPI-KEY", "22fe836a6bmsh1d796034ff46278p10a23cjsn59c17d10c169");
            Client.DefaultRequestHeaders.Add("X-RAPIDAPI-HOST", "apidojo-yahoo-finance-v1.p.rapidapi.com");
        }

        public float GetCurrentPrice(string symbol)
        {
            HttpResponseMessage response = Client.GetAsync($"/stock/v2/get-summary?symbol={symbol}").Result;

            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                
                Response yahooResponse = Serializer.Deserialize<Response>(result);

                float fmt = yahooResponse.price.regularMarketPrice.fmt;
                return fmt;
            }

            return -1F;
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
using Model;
using Nancy.Json;
using Reader;
using QuoteAPI;
using QuoteManager;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Timers;
using System.Net;
using System.Net.Mail;

namespace MonitorCotacaoB3
{
    class Program
    {

        static void Main(string[] args)
        {
            //ConsoleReader consoleReader = new ConsoleReader();
            //StockPrices stockPrices = consoleReader.readStockPrices();

            //StockQuoteTimer timer = new StockQuoteTimer(6000);
            //timer.StartTimer(stockPrices);

            QuoteMail quoteMail = new QuoteMail();
            quoteMail.SendMail("teste sub", "test body");
        }

    }

    class StockQuoteTimer
    {
        private readonly QuotePriceChecker quotePriceChecker = new QuotePriceChecker();
        private readonly int millis;

        public StockQuoteTimer(int millis)
        {
            this.millis = millis;
        }

        public void StartTimer(StockPrices stockPrices)
        {
            Timer timer = new Timer(millis);

            timer.Elapsed += (s, eventTime) => {
                Console.WriteLine("Querying quote price at {0:HH:mm:ss.fff}", eventTime.SignalTime);
                quotePriceChecker.check(stockPrices);
            };
            timer.AutoReset = true;
            timer.Enabled = true;

            Console.ReadLine();
            timer.Stop();
            timer.Dispose();
        }
    }

    public class QuoteMail
    {

        private string emailFrom;
        private string emailTo;
        private SmtpClient client;

        public QuoteMail()
        {
            emailFrom = "a@gmail.com";
            emailTo = "b@gmail.com";

            client = new SmtpClient("smtp.mailtrap.io", 2525)
            {
                Credentials = new NetworkCredential("18c2fcbc7ad625", "5a0a1eb6970b3c"),
                EnableSsl = true
            };
        }

        public void SendMail(string subject, string message)
        {
            client.Send(emailFrom, emailTo, subject, message);
            Console.WriteLine("Email sent");
        }
    }

}

namespace Reader
{
    public class ConsoleReader
    {
        public StockPrices readStockPrices()
        {
            StockPrices stockPrices = new StockPrices();

            // Read from console

            stockPrices.symbol = "PETR4.SA";
            stockPrices.salePrice = 32.67F;
            stockPrices.purchasePrice = 29.59F;
            return stockPrices;
        }
    }
}

namespace QuoteManager
{

    class QuotePriceChecker
    {
        private readonly YahooFinanceAPI api = new YahooFinanceAPI();
        private readonly QuoteRecommend recommend = new QuoteRecommend();

        public void check(StockPrices stockPrices)
        {
            float price = api.GetCurrentPrice(stockPrices.symbol);

            if (price >= 0F)
            {
                if (price <= stockPrices.purchasePrice)
                {
                    recommend.purchase();
                }
                else if (price >= stockPrices.salePrice)
                {
                    recommend.sale();
                }
            }
        }
    }

    class QuoteRecommend
    {

        public void purchase()
        {
            Console.WriteLine("Comprar");
        }

        public void sale()
        {
            Console.WriteLine("Vender");
        }
    }
}

namespace QuoteAPI
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

    public class StockPrices
    {
        public string symbol { get; set; }

        public float salePrice { get; set; }
        
        public float purchasePrice { get; set; }

    }

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
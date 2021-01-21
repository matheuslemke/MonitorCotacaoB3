using System;
using System.Timers;

namespace QuoteManager
{
    public class StockQuoteTimer
    {
        private readonly QuotePriceChecker quotePriceChecker = new QuotePriceChecker();
        private readonly int millis;

        public StockQuoteTimer(int millis)
        {
            this.millis = millis;
        }

        public void StartTimer(Model.StockPrices stockPrices)
        {
            Timer timer = new Timer(millis);
            Console.WriteLine("Starting application...");

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
}

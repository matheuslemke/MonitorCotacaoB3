using Model;
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

        public void StartTimer(StockPrices stockPrices)
        {
            Timer timer = new Timer(millis);
            Console.WriteLine("\nStarting application...");

            timer.Elapsed += (s, eventTime) => {
                Console.WriteLine($"Querying {stockPrices.Symbol} price at {eventTime.SignalTime}");
                quotePriceChecker.Check(stockPrices);
            };
            timer.AutoReset = true;
            timer.Enabled = true;

            Console.ReadLine();
            timer.Stop();
            timer.Dispose();
        }
    }
}

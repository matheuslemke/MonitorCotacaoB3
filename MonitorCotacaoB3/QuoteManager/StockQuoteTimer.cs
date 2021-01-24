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
            Console.Write("Iniciando aplicação - ");
            quotePriceChecker.Check(stockPrices);

            timer.Elapsed += (s, eventTime) => {
                Console.Write($"{eventTime.SignalTime} - ");
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

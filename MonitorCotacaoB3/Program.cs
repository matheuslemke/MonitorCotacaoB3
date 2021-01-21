using Model;
using Reader;
using QuoteManager;
using System;

namespace MonitorCotacaoB3
{
    class Program
    {

        static void Main(string[] args)
        {
            try
            {
                ConsoleReader consoleReader = new ConsoleReader();
                StockPrices stockPrices = consoleReader.ReadStockPrices();

                StockQuoteTimer timer = new StockQuoteTimer(10000);
                timer.StartTimer(stockPrices);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

    }
    

}



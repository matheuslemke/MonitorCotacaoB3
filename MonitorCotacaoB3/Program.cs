using Model;
using Nancy.Json;
using Reader;
using QuoteAPI;
using QuoteManager;
using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Timers;
using System.Net;
using System.Net.Mail;
using MonitorCotacaoB3;

namespace MonitorCotacaoB3
{
    class Program
    {

        static void Main(string[] args)
        {
            ConsoleReader consoleReader = new ConsoleReader();
            StockPrices stockPrices = consoleReader.readStockPrices();

            StockQuoteTimer timer = new StockQuoteTimer(15000);
            timer.StartTimer(stockPrices);
        }

    }



    

}



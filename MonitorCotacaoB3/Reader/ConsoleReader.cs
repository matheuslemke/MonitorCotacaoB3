using Model;
using System;

namespace Reader
{
    public class ConsoleReader
    {
        public StockPrices ReadStockPrices()
        {
            StockPrices stockPrices = new StockPrices();

            Console.WriteLine("Enter symbol, selling price and purchase price separated by space...\n");

            string inputLine = Console.ReadLine();
                
            if (!string.IsNullOrEmpty(inputLine))
            {
                string[] args = inputLine.Split(" ");
                stockPrices.Symbol = args[0];
                stockPrices.SalePrice = float.Parse(args[1]);
                stockPrices.PurchasePrice = float.Parse(args[2]);
            }
            else
            {
                throw new Exception("Problem with input");
            }

            return stockPrices;
        }
    }
}

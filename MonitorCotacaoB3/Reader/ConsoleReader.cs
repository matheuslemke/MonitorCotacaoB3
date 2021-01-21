using Model;
using System;

namespace Reader
{
    public class ConsoleReader
    {
        public StockPrices readStockPrices()
        {
            StockPrices stockPrices = new StockPrices();

            try
            {
                stockPrices.Symbol = Console.ReadLine();
                stockPrices.SalePrice = Console.Read();
                stockPrices.PurchasePrice = Console.Read();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return stockPrices;
        }
    }
}

using Model;
using System;

namespace Reader
{

    public class ConsoleReader
    {
        private const string INPUT_READ_PROBLEM = "Problema ao ler entrada.";

        public StockPrices ReadStockPrices()
        {
            StockPrices stockPrices = new StockPrices();

            Console.WriteLine("Digite o ATIVO a ser monitorado, o PREÇO DE VENDA e o PREÇO DE COMPRA, separados por espaço.");
            Console.WriteLine("Ex: PETR4.SA 22,67 22,59\n");

            string inputLine = Console.ReadLine();
                
            if (!string.IsNullOrEmpty(inputLine))
            {
                string[] args = inputLine.Split(" ");
                stockPrices.Symbol = args[0];
                stockPrices.SalePrice = GetFloat(args[1]);
                stockPrices.PurchasePrice = GetFloat(args[2]);
            }
            else
            {
                throw new Exception(INPUT_READ_PROBLEM);
            }

            return stockPrices;
        }

        private float GetFloat(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new Exception(INPUT_READ_PROBLEM);
            }
            input = input.Replace(".", ",");

            return float.Parse(input);
        }

    }
}

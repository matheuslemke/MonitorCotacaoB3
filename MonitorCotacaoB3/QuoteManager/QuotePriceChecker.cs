using Model;
using QuoteAPI;

namespace QuoteManager
{
    class QuotePriceChecker
    {
        private readonly YahooFinanceAPI api = new YahooFinanceAPI();
        private readonly RecommenderFactory recommenderFactory = new RecommenderFactory();

        public void Check(StockPrices stockPrices)
        {
            float price = api.GetCurrentPrice(stockPrices.Symbol);

            recommenderFactory.Recommend(stockPrices, price);
        }
    }
}

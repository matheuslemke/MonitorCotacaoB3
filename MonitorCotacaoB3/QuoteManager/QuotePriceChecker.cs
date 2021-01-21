using QuoteAPI;

namespace QuoteManager
{
    class QuotePriceChecker
    {
        private readonly YahooFinanceAPI api = new YahooFinanceAPI();
        private readonly QuoteRecommend recommend = new QuoteRecommend();

        public void check(Model.StockPrices stockPrices)
        {
            float price = api.GetCurrentPrice(stockPrices.Symbol);

            if (price >= 0F)
            {
                if (price <= stockPrices.PurchasePrice)
                {
                    recommend.Purchase();
                }
                else if (price >= stockPrices.SalePrice)
                {
                    recommend.Sale();
                }
            }
        }
    }
}

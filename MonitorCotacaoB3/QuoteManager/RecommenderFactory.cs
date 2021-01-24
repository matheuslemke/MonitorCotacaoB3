using Model;

namespace QuoteManager
{
    
    class RecommenderFactory
    {
        private readonly PurchaseRecommender purchaseRecommender;
        private readonly SaleRecommender saleRecommender;

        public RecommenderFactory()
        {
            purchaseRecommender = new PurchaseRecommender();
            saleRecommender = new SaleRecommender();
        }

        public void Recommend(StockPrices stockPrices, float price)
        {
            if (ShouldRecommendPurchase(stockPrices, price))
            {
                purchaseRecommender.Recommend(stockPrices, price);
            }
            else if (ShouldRecommendSale(stockPrices, price))
            {
                saleRecommender.Recommend(stockPrices, price);
            }
        }

        private bool ShouldRecommendPurchase(StockPrices stockPrices, float price)
        {
            return price >= 0F && price <= stockPrices.PurchasePrice;
        }

        private bool ShouldRecommendSale(StockPrices stockPrices, float price)
        {
            return price >= 0F && price >= stockPrices.SalePrice;
        }
    }
}

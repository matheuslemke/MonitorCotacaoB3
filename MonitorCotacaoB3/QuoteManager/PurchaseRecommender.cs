using Model;

namespace QuoteManager
{
    class PurchaseRecommender : Recommender
    {
        protected override string GetSubject(StockPrices stockPrices)
        {
            return $"{stockPrices.Symbol} - Queda do preço";
        }

        protected override string GetMessage(StockPrices stockPrices, float price)
        {
            return $"Olá. O ativo {stockPrices.Symbol} baixou o preço e está custando R$ {price}. Recomenda-se a compra nesse momento. Att.";
        }
    }
}


using Model;

namespace QuoteManager
{
    class SaleRecommender : Recommender
    {

        protected override string GetSubject(StockPrices stockPrices)
        {
            return $"{stockPrices.Symbol} - Alta do preço";
        }

        protected override string GetMessage(StockPrices stockPrices, float price)
        {
            return $"Olá. O ativo {stockPrices.Symbol} está com o preço mais alto, custando R$ {price}. Chegou a hora de vendê-lo. Att.";
        }

    }
}

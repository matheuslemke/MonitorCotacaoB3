using Model;

namespace QuoteManager
{

    abstract class Recommender
    {
        private readonly QuoteMail mail = new QuoteMail();

        public QuoteMail Mail => mail;

        public void Recommend(StockPrices stockPrices, float price)
        {
            string subject = GetSubject(stockPrices);
            string message = GetMessage(stockPrices, price);

            Mail.SendMail(subject, message);
        }

        protected abstract string GetSubject(StockPrices stockPrices);

        protected abstract string GetMessage(StockPrices stockPrices, float price);
    }

}

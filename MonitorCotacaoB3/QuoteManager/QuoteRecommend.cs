using MonitorCotacaoB3;

namespace QuoteManager
{
    class QuoteRecommend
    {
        private readonly QuoteMail mail = new QuoteMail();

        public void Purchase()
        {
            mail.SendMail("Comprar", "Comprar");
        }

        public void Sale()
        {
            mail.SendMail("Vender", "Vender");
        }
    }
}

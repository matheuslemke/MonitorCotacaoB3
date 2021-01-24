# Monitor de Cotação - B3

Monitor de Cotação - B3 é uma aplicação que consulta o preço de um ativo na B3. Dados os preços de venda e de compra, a aplicação recomenda via e-mail caso tenha uma alteração.

As configurações de e-mail são informadas via [Arquivo de configuração](MonitorCotacaoB3/App.config). Com a finalidade de testes, foi utilizada a API do [Mailtrap](https://mailtrap.io/) para simular uma caixa de entrada/saída.

A API de consulta é fornecida pelo Yahoo Finance ([Documentação](https://rapidapi.com/apidojo/api/yahoo-finance1/endpoints)). Mais detalhes na classe [YahooFinanceAPI](MonitorCotacaoB3/QuoteAPI/YahooFinanceAPI.cs).

## Usage

```bash
Digite o ATIVO a ser monitorado, o PREÇO DE VENDA e o PREÇO DE COMPRA, separados por espaço.
Ex: PETR4.SA 22,67 22,59

PETR4.SA 22,67 22,59
```

## Contributing
Pull requests são bem-vindos.

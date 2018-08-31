# RadixAPIGateway
Projeto de teste para processo seletivo da Radix

## Objetivo
Criar um gateway e-commerce com endpoint único para facilitar requisições com operações de cartão de crédito através das adquirentes Stone ou Cielo.

### Desenvolvimento utilizando-se de:
- Visual Studio Community 2017;
- .Net Core;
- Banco de Dados LocalDb (SQL Server);
- Layered Architecture;
- API Rest;
- Code First; e
- Injeção de dependência

# Métodos da API
## GET
URL: "http://<server>[:port]/api/transaction/search/store/{idStore}", onde {idStore} é o identificador da loja para a qual se quer consultar as transações de vendas com cartões de crédito realizadas para ela
```
http://localhost/api/transaction/search/store/1
```

Este método retorna um Json com as transações da loja informada, no seguinte formato
```
{
    "response": [
        {
            "id": 3,
            "date": "2018-08-01T00:00:00",
            "total": 538,
            "storeId": 2,
            "store": null
        },
        {
            "id": 4,
            "date": "2018-08-02T00:00:00",
            "total": 300.35,
            "storeId": 2,
            "store": null
        },
        {
            "id": 5,
            "date": "2018-08-03T00:00:00",
            "total": 345,
            "storeId": 2,
            "store": null
        }
    ],
    "message": "Foram encontradas transações de venda para a loja informada"
}
```
ou, caso não haja transações
```
{
    "response": [],
    "message": "Foram encontradas transações de venda para a loja informada"
}
```

## POST
URL: "http://<server>[:port]/api/transaction/sale", passando no corpo da requisição um Json no formato abaixo
```
{
  "idLoja": 1,
	"data": "07/15/2018",
	"transacao": {
		"valor": 130,
		"parcelas": 6,
		"numeroVenda": "0001/2018",
		"cartaoCredito": {
			"anoExpiracao": 2025,
			"mesExpiracao": 10,
			"NumeroCartao": "1234123412341234",
			"codigoSeguranca": "123",
			"nomeImpressoCartao": "JOÃO DA SILVA"
		}
	}
}
```

### Autor
Flavio Rianelli

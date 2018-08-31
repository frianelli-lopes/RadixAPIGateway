using System.Net.Http;
using System.Threading.Tasks;
using RadixAPIGateway.Domain.Interfaces.Providers;
using RadixAPIGateway.Domain.Models;
using RadixAPIGateway.Domain.Models.EnumTypes;
using RadixAPIGateway.Domain.Models.Request;

namespace RadixAPIGateway.Provider.SaleTransaction
{
    public class SaleTransactionProvider : ISaleTransactionProvider
    {
        public Task<HttpResponseMessage> SendRequest(Store store, SaleRequest request)
        {
            AcquirerEnum? acquirerEnum = store.Acquirer;

            if (!acquirerEnum.HasValue)
            {
                if (request.Transacao.CartaoCredito.Bandeira == CreditCardBrandEnum.Visa)
                    acquirerEnum = AcquirerEnum.Stone;
                else
                    acquirerEnum = AcquirerEnum.Cielo;
            }

            if (acquirerEnum == AcquirerEnum.Cielo)
            {

            }

            HttpClient client = new HttpClient();
            var jsonInString = JsonConvert.SerializeObject(transactionToSend);
            HttpResponseMessage response = await client.PostAsync(uri, new StringContent(jsonInString, Encoding.UTF8, "application/json"));


        }
    }
}

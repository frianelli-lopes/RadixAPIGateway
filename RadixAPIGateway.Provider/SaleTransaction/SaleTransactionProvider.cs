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
        public async Task<HttpResponseMessage> SendRequest(Store store, SaleRequest saleRequest)
        {
            int? idAcquirer = store.IdAcquirer;

            if (!idAcquirer.HasValue)
            {
                if (saleRequest.Transacao.CartaoCredito.Bandeira == CreditCardBrandEnum.Visa)
                    idAcquirer = 2;
                else
                    idAcquirer = 1;
            }

            var acquirer = Acquirer.GetAcquirerById(idAcquirer.Value);

            var response = await acquirer.SendRequest(store, saleRequest);

            return response;
        }
    }
}

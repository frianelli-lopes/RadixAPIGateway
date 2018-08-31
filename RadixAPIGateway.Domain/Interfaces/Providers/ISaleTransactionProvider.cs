using RadixAPIGateway.Domain.Models;
using RadixAPIGateway.Domain.Models.Request;
using System.Net.Http;
using System.Threading.Tasks;

namespace RadixAPIGateway.Domain.Interfaces.Providers
{
    public interface ISaleTransactionProvider
    {
        Task<HttpResponseMessage> SendRequest(Store store, SaleRequest request);
    }
}

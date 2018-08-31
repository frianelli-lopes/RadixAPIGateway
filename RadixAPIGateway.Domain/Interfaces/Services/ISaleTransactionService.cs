using RadixAPIGateway.Domain.Models;
using RadixAPIGateway.Domain.Models.Request;
using RadixAPIGateway.Domain.Shareds.Results;
using System.Threading.Tasks;

namespace RadixAPIGateway.Domain.Interfaces.Services
{
    public interface ISaleTransactionService
    {
        Task<OperationResult> Process(SaleRequest request);
        Task<GetManyResult<SaleTransaction>> GetByStore(int idStore);
    }
}

using RadixAPIGateway.Domain.Models.Request;
using RadixAPIGateway.Domain.Shareds.Results;
using System.Threading.Tasks;

namespace RadixAPIGateway.Domain.Interfaces.Services
{
    public interface ISaleService
    {
        Task<OperationResult> Process(SaleRequest request);
    }
}

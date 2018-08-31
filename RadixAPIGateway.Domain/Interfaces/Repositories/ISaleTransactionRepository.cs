using RadixAPIGateway.Domain.Models;
using RadixAPIGateway.Domain.Shareds.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RadixAPIGateway.Domain.Interfaces.Repositories
{
    public interface ISaleTransactionRepository
    {
        Task<IEnumerable<SaleTransaction>> GetByStore(int idStore);
    }
}

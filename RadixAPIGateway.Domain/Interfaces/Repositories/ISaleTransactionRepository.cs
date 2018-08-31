using RadixAPIGateway.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RadixAPIGateway.Domain.Interfaces.Repositories
{
    public interface ISaleTransactionRepository
    {
        Task<IEnumerable<SaleTransaction>> GetByStore(int idStore);
    }
}

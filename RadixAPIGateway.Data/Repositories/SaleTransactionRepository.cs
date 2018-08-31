using Microsoft.EntityFrameworkCore;
using RadixAPIGateway.Data.Context;
using RadixAPIGateway.Domain.Interfaces.Repositories;
using RadixAPIGateway.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RadixAPIGateway.Data.Repositories
{
    public class SaleTransactionRepository : ISaleTransactionRepository
    {
        private readonly EFContext _context;

        public SaleTransactionRepository(EFContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SaleTransaction>> GetByStore(int idStore)
        {
            return await _context.Set<SaleTransaction>().Where(x => x.StoreId == idStore).ToListAsync();
        }
    }
}

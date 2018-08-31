using RadixAPIGateway.Data.Context;
using RadixAPIGateway.Domain.Interfaces.Repositories.Generic;
using RadixAPIGateway.Domain.Shareds.Results;
using System.Threading.Tasks;

namespace RadixAPIGateway.Data.Repositories.Generic
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly EFContext _context;

        public GenericRepository(EFContext context)
        {
            _context = context;
        }

        public async virtual Task<TEntity> GetById(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }
    }
}

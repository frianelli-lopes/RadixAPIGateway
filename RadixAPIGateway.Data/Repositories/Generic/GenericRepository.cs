using RadixAPIGateway.Data.Context;
using RadixAPIGateway.Domain.Interfaces.Repositories.Generic;
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

        public virtual Task<TEntity> GetById(int id)
        {
            return _context.Set<TEntity>().FindAsync(id);
        }
    }
}

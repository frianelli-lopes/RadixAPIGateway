using System.Threading.Tasks;

namespace RadixAPIGateway.Domain.Interfaces.Repositories.Generic
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetById(int id);
    }
}

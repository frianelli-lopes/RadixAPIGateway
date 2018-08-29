using RadixAPIGateway.Domain.Shareds.Results;
using System.Threading.Tasks;

namespace RadixAPIGateway.Domain.Interfaces.Services.Generic
{
    public interface IGenericService<TEntity> where TEntity : class
    {
        Task<GetOneResult<TEntity>> GetById(int id);
    }
}

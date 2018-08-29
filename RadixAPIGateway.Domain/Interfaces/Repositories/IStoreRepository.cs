using RadixAPIGateway.Domain.Models;

namespace RadixAPIGateway.Domain.Interfaces.Repositories
{
    public interface IStoreRepository
    {
        Store GetById(int id);
    }
}

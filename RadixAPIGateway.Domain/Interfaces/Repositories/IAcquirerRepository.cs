using RadixAPIGateway.Domain.Models;

namespace RadixAPIGateway.Domain.Interfaces.Repositories
{
    public interface IAcquirerRepository
    {
        Acquirer GetById(int id);
    }
}

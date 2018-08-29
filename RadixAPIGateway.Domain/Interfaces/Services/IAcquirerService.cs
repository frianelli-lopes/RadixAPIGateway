using RadixAPIGateway.Domain.Models;

namespace RadixAPIGateway.Domain.Interfaces.Services
{
    public interface IAcquirerService
    {
        Acquirer GetById(int id);
    }
}

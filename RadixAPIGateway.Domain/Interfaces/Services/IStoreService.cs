using RadixAPIGateway.Domain.Models;

namespace RadixAPIGateway.Domain.Interfaces.Services
{
    public interface IStoreService
    {
        Store GetById(int id);
    }
}

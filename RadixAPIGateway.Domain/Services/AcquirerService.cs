using RadixAPIGateway.Domain.Interfaces.Repositories;
using RadixAPIGateway.Domain.Interfaces.Services;
using RadixAPIGateway.Domain.Models;
using RadixAPIGateway.Domain.Services.Generic;

namespace RadixAPIGateway.Domain.Services
{
    public class AcquirerService : GenericService<Acquirer>, IAcquirerService
    {
        public AcquirerService(IAcquirerRepository repository) : base(repository)
        {
        }
    }
}

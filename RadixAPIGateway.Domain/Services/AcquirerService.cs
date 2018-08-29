using RadixAPIGateway.Domain.Interfaces.Repositories;
using RadixAPIGateway.Domain.Interfaces.Services;
using RadixAPIGateway.Domain.Models;
using System;

namespace RadixAPIGateway.Domain.Services
{
    public class AcquirerService : IAcquirerService
    {
        private readonly IAcquirerRepository _acquireRepository;

        public AcquirerService(IAcquirerRepository acquireRepository)
        {
            _acquireRepository = acquireRepository;
        }

        public Acquirer GetById(int id)
        {
            return _acquireRepository.GetById(id);
        }
    }
}

using RadixAPIGateway.Domain.Interfaces.Repositories;
using RadixAPIGateway.Domain.Interfaces.Services;
using RadixAPIGateway.Domain.Models;
using System;

namespace RadixAPIGateway.Domain.Services
{
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _storeRepository;

        public StoreService(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        public Store GetById(int id)
        {
            return _storeRepository.GetById(id);
        }
    }
}

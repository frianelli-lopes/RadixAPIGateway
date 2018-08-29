using RadixAPIGateway.Data.Context;
using RadixAPIGateway.Data.Repositories.Generic;
using RadixAPIGateway.Domain.Interfaces.Repositories;
using RadixAPIGateway.Domain.Models;

namespace RadixAPIGateway.Data.Repositories
{
    public class StoreRepository : GenericRepository<Store>, IStoreRepository
    {
        public StoreRepository(EFContext context) : base(context)
        {
        }
    }
}

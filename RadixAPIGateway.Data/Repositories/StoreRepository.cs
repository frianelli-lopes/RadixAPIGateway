using RadixAPIGateway.Domain.Interfaces.Repositories;
using RadixAPIGateway.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RadixAPIGateway.Data.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        private  EFContext Db = new EFContext();
        public StoreRepository()
        {

        }

        public Store GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}

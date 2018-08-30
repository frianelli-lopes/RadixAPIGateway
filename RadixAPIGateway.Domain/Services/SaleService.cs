using RadixAPIGateway.Domain.Interfaces.Repositories;
using RadixAPIGateway.Domain.Interfaces.Services;
using RadixAPIGateway.Domain.Models.Request;
using RadixAPIGateway.Domain.Shareds.Results;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RadixAPIGateway.Domain.Services
{
    public class SaleService : ISaleService
    {
        private readonly IStoreService _storeService;

        public SaleService(IStoreService storeService)
        {
            _storeService = storeService;
        }

        public async Task<OperationResult> Process(SaleRequest request)
        {
            var oneResultStore = _storeService.GetById(request.IdLoja);

            if (!oneResultStore.Result.Success)
            {
                var resultStore = oneResultStore.Result;
                return new OperationResult(false, resultStore.Message, resultStore.StatusCode, resultStore.Exception);
            }

            return new OperationResult(true, null, System.Net.HttpStatusCode.OK, null);
        }
    }
}

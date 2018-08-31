using RadixAPIGateway.Domain.Interfaces.Providers;
using RadixAPIGateway.Domain.Interfaces.Services;
using RadixAPIGateway.Domain.Models;
using RadixAPIGateway.Domain.Models.EnumTypes;
using RadixAPIGateway.Domain.Models.Request;
using RadixAPIGateway.Domain.Shareds.Results;
using System.Threading.Tasks;

namespace RadixAPIGateway.Domain.Services
{
    public class SaleTransactionService : ISaleTransactionService
    {
        private readonly IStoreService _storeService;
        private readonly IAntiFraudProvider _antiFraudProvider;
        private readonly ISaleTransactionProvider _saleTransactionProvider;

        public SaleTransactionService(IStoreService storeService, IAntiFraudProvider antiFraudProvider)
        {
            _storeService = storeService;
            _antiFraudProvider = antiFraudProvider;
        }

        public async Task<OperationResult> Process(SaleRequest request)
        {
            var oneResultStore = await _storeService.GetById(request.IdLoja);

            if (!oneResultStore.Success)
                return new OperationResult(false, oneResultStore.Message, oneResultStore.StatusCode, oneResultStore.Exception);

            if (!ProcessAntiFraud(oneResultStore.Entity, request))
                return new OperationResult(false, "Erro no processamento antifraude", System.Net.HttpStatusCode.BadRequest, oneResultStore.Exception);

            if (!ProcessSaleTransaction(oneResultStore.Entity, request))
                return new OperationResult(false, "Erro no processamento da transação de venda", System.Net.HttpStatusCode.BadRequest, oneResultStore.Exception);
            
            return new OperationResult(true, null, System.Net.HttpStatusCode.OK, null);
        }

        private bool ProcessAntiFraud(Store store, SaleRequest request)
        {
            if (store.HasAntiFraudAgreement) return _antiFraudProvider.IsSecure(request);

            return true;
        }

        private bool ProcessSaleTransaction(Store store, SaleRequest request)
        {
            return _saleTransactionProvider.
        }
    }
}

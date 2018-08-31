using RadixAPIGateway.Domain.Interfaces.Providers;
using RadixAPIGateway.Domain.Interfaces.Repositories;
using RadixAPIGateway.Domain.Interfaces.Services;
using RadixAPIGateway.Domain.Models;
using RadixAPIGateway.Domain.Models.Request;
using RadixAPIGateway.Domain.Shareds.Results;
using System.Net.Http;
using System.Threading.Tasks;

namespace RadixAPIGateway.Domain.Services
{
    public class SaleTransactionService : ISaleTransactionService
    {
        private readonly IStoreService _storeService;
        private readonly IAntiFraudProvider _antiFraudProvider;
        private readonly ISaleTransactionProvider _saleTransactionProvider;
        private readonly ISaleTransactionRepository _saleTransactionRepository;

        public SaleTransactionService(IStoreService storeService, IAntiFraudProvider antiFraudProvider, ISaleTransactionProvider saleTransactionProvider, ISaleTransactionRepository saleTransactionRepository)
        {
            _storeService = storeService;
            _antiFraudProvider = antiFraudProvider;
            _saleTransactionProvider = saleTransactionProvider;
            _saleTransactionRepository = saleTransactionRepository;
        }

        public async Task<GetManyResult<SaleTransaction>> GetByStore(int idStore)
        {
            try
            {
                var result = await _saleTransactionRepository.GetByStore(idStore);

                if (result == null)
                {
                    return new GetManyResult<SaleTransaction>(null, false, "Não foram encontradas transações de venda para a loja informada", System.Net.HttpStatusCode.NotFound, null);
                }
                else
                {
                    return new GetManyResult<SaleTransaction>(result, true, "Foram encontradas transações de venda para a loja informada", System.Net.HttpStatusCode.OK, null);
                }
            }
            catch (System.Exception ex)
            {
                return new GetManyResult<SaleTransaction>(null, false, "Erro ao tentar listar transações de venda para a loja informada", System.Net.HttpStatusCode.BadRequest, ex);
            }
        }

        public async Task<OperationResult> Process(SaleRequest request)
        {
            var oneResultStore = await _storeService.GetById(request.IdLoja);

            if (!oneResultStore.Success)
                return new OperationResult(false, oneResultStore.Message, oneResultStore.StatusCode, oneResultStore.Exception);

            if (!ProcessAntiFraud(oneResultStore.Entity, request))
                return new OperationResult(false, "Erro no processamento antifraude", System.Net.HttpStatusCode.BadRequest, oneResultStore.Exception);

            var response = await ProcessSaleTransaction(oneResultStore.Entity, request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK) 
                return new OperationResult(false, "Erro no processamento da transação de venda", response.StatusCode, oneResultStore.Exception);
            
            return new OperationResult(true, null, System.Net.HttpStatusCode.OK, null);
        }

        private bool ProcessAntiFraud(Store store, SaleRequest request)
        {
            if (store.HasAntiFraudAgreement) return _antiFraudProvider.IsSecure(request);

            return true;
        }

        private async Task< HttpResponseMessage> ProcessSaleTransaction(Store store, SaleRequest request)
        {
            return await _saleTransactionProvider.SendRequest(store, request);
        }
    }
}

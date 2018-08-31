using Microsoft.AspNetCore.Mvc;
using RadixAPIGateway.Domain.Interfaces.Services;
using RadixAPIGateway.Domain.Models;
using RadixAPIGateway.Domain.Models.Request;
using RadixAPIGateway.Domain.Shareds.Results;
using System.Threading.Tasks;

namespace RadixAPIGateway.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IStoreService _storeService;
        private readonly ISaleTransactionService _saleTransactionService;

        public TransactionController(IStoreService storeService, ISaleTransactionService saleTransactionService)
        {
            _storeService = storeService;
            _saleTransactionService = saleTransactionService;
        }

        [HttpGet("search/store/{idStore}")]
        public ApiResult<SaleTransaction> GetByStore(int idStore)
        {
            var result = _saleTransactionService.GetByStore(idStore);

            return new ApiResult<SaleTransaction>(result.Result);
        }

        [HttpPost("sale")]
        public async Task<ApiResult<SaleRequest>> CreateSaleAsync([FromBody] SaleRequest request)
        {
            if (!ModelState.IsValid) return new ApiResult<SaleRequest>(new OperationResult(false, ModelState.ToString(), System.Net.HttpStatusCode.BadRequest, null));

            var result = await _saleTransactionService.Process(request);

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
                return new ApiResult<SaleRequest>(new OperationResult(false, result.Message, result.StatusCode, result.Exception));

            return new ApiResult<SaleRequest>(new OperationResult(true, "Transação realizada com sucesso", System.Net.HttpStatusCode.OK, null));
        }
    }
}
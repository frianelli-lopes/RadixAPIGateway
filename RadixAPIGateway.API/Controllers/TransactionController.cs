using Microsoft.AspNetCore.Mvc;
using RadixAPIGateway.Domain.Interfaces.Services;
using RadixAPIGateway.Domain.Models;
using RadixAPIGateway.Domain.Models.Request;
using RadixAPIGateway.Domain.Shareds.Results;

namespace RadixAPIGateway.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IStoreService _storeService;
        private readonly ISaleService _saleService;

        public TransactionController(IStoreService storeService, ISaleService saleService)
        {
            _storeService = storeService;
            _saleService = saleService;
        }

        [HttpGet("search/store/{id}")]
        public ApiResult<Store> GetById(int id)
        {
            var result = _storeService.GetById(id);

            return new ApiResult<Store>(result.Result);
        }

        [HttpPost("sale")]
        public ApiResult<SaleRequest> CreateSaleAsync([FromBody] SaleRequest request)
        {
            if (!ModelState.IsValid) return new ApiResult<SaleRequest>(new OperationResult(false, ModelState.ToString(), System.Net.HttpStatusCode.BadRequest, null));

            _saleService.Process(request);

            return new ApiResult<SaleRequest>(new OperationResult(true, "Transação realizada com sucesso", System.Net.HttpStatusCode.OK, null));
            //if (request == null)
            //    return new HttpResult<Loja>().Set(HttpStatusCode.BadRequest, $"Erro na requisição");
            //return await _transactionAppService.CreateSaleAsync(request);
        }
    }
}
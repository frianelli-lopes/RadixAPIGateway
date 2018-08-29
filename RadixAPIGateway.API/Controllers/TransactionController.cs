using Microsoft.AspNetCore.Mvc;
using RadixAPIGateway.Domain.Interfaces.Services;
using RadixAPIGateway.Domain.Models;

namespace RadixAPIGateway.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IStoreService _storeService;

        public TransactionController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        [HttpGet("search/store/{idStore}")]
        public IActionResult GetAll(int idStore)
        {
            //var result = _storeService.GetById(idStore);

            //return new ApiResult<Store>(result.Result);

            //return Ok(result.Result.Entity);

            return Ok("Teste");
        }

        //[HttpPost]
        //public string Transacao()
        //{
        //    return "Transacao";
        //}
    }
}
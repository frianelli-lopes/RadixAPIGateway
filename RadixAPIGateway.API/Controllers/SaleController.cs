using Microsoft.AspNetCore.Mvc;

namespace RadixAPIGateway.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        [HttpGet]
        public string GetAll()
        {
            

        }

        [HttpPost]
        public string Transacao()
        {
            return "Transacao";
        }
    }
}
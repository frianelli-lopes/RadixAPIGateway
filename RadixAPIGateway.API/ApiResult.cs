using Microsoft.AspNetCore.Mvc;
using RadixAPIGateway.Domain.Shareds.Results;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace RadixAPIGateway.API
{
    public class ApiResult<TResponse> : IActionResult where TResponse : class
    {
        public IEnumerable<TResponse> Response { get; private set; }
        public string Message { get; private set; }
        private HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;

        public ApiResult(OperationResult result)
        {
            string typeResult = result.GetType().Name;

            if (typeResult.Contains("GetOneResult"))
            {
                GetOneResult<TResponse> oneResult = (GetOneResult <TResponse>)result;
                if (oneResult != null && oneResult.Entity != null) this.Response = new List<TResponse>() { oneResult.Entity };
                
            } else if (typeResult.Contains("GetManyResult"))
            {
                GetManyResult<TResponse> manyResult = (GetManyResult<TResponse>)result;
                if (manyResult != null && manyResult.Entities != null) this.Response = manyResult.Entities;

            }

            this.Message = result.Message;
            this.StatusCode = result.StatusCode;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult(this) { StatusCode = (int)StatusCode };
            await objectResult.ExecuteResultAsync(context);
        }
    }
}

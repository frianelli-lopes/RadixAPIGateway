using System;
using System.Collections.Generic;
using System.Net;

namespace RadixAPIGateway.Domain.Shareds.Results
{
    public class GetManyResult<TEntity> : OperationResult where TEntity : class
    {
        public IEnumerable<TEntity> Entities { get;  private set; }

        public GetManyResult(IEnumerable<TEntity> entities, bool success, string message, HttpStatusCode statusCode, Exception exception) 
            : base(success, message, statusCode, exception)
        {
            this.Entities = entities;
        }
    }        
}

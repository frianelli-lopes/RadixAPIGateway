using System;
using System.Net;

namespace RadixAPIGateway.Domain.Shareds.Results
{
    public class GetOneResult<TEntity> : OperationResult where TEntity : class
    {
        public TEntity Entity { get; private set; }

        public GetOneResult(TEntity entity, bool success, string message, HttpStatusCode statusCode, Exception exception) 
            : base(success, message, statusCode, exception)
        {
            this.Entity = entity;
        }
    }
}

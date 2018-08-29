using System;
using System.Net;

namespace RadixAPIGateway.Domain.Shareds.Results
{
    public class OperationResult
    {
        public bool Success { get; private set; }
        public string Message { get; private set; }
        public HttpStatusCode StatusCode { get; private set; }
        public Exception Exception { get; private set; }

        public OperationResult(bool success, string message, HttpStatusCode statusCode, Exception exception)
        {
            this.Success = success;
            this.Message = message;
            this.StatusCode = statusCode;
            this.Exception = exception;
        }
    }
}

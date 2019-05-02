using System.Collections.Generic;

namespace SharePointGateway.Core
{
    public class OperationResult<T>
    {
        public bool Success;

        public string ErrorMessage;

        public IEnumerable<T> Result;
    }
}
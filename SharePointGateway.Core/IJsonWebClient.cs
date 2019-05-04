using System.Net;
using Newtonsoft.Json.Linq;

namespace SharePointGateway.Core
{
    public interface IJsonWebClient
    {
        OperationResult<JToken> Get(string requestUri, ICredentials networkCredentials, int maxResults);
    }
}
using System.Net;

namespace SharePointGateway.Core
{
    public interface IWebClient
    {
        string Get(string requestUri, ICredentials networkCredential);
    }
}
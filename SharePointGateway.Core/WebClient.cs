using System;
using System.IO;
using System.Net;

namespace SharePointGateway.Core
{
    public class WebClient : IWebClient
    {
        public string Get(string requestUri, ICredentials networkCredential)
        {
            HttpWebRequest endpointRequest = (HttpWebRequest)WebRequest.Create(requestUri);

            endpointRequest.Method = "GET";
            endpointRequest.Accept = "application/json;odata=verbose";

            endpointRequest.Credentials = networkCredential;

            var webResponse = endpointRequest.GetResponse();
            using (var webStream = webResponse.GetResponseStream())
            {
                if (webStream == null)
                {
                    throw new ApplicationException("webStream is null");
                }

                using (var responseReader = new StreamReader(webStream))
                {
                    return responseReader.ReadToEnd();
                }
            }
        }
    }
}

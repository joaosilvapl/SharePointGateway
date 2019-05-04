using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json.Linq;

namespace SharePointGateway.Core
{
    public class JsonWebClient : IJsonWebClient
    {
        private readonly IWebClient _webClient;

        public JsonWebClient(IWebClient webClient)
        {
            this._webClient = webClient;
        }

        public OperationResult<JToken> Get(string requestUri, ICredentials networkCredentials, int maxResults)
        {
            var allItems = new List<JToken>();

            do
            {
                var result = this.Get(requestUri, networkCredentials);

                if (!result.Success)
                {
                    return result;
                }

                if (result.Result != null)
                {
                    allItems.AddRange(result.Result);
                }

                requestUri = result.NextRequestUri;

                if (allItems.Count >= maxResults || string.IsNullOrWhiteSpace(requestUri))
                {
                    break;
                }

            } while (true);

            return new OperationResult<JToken> { Success = true, Result = allItems };
        }

        private GetOperationResult Get(string requestUri, ICredentials networkCredential)
        {
            try
            {
                var itemList = new List<JToken>();

                string response = this._webClient.Get(requestUri, networkCredential);
                return ParseResponse(response, itemList);
            }
            catch (Exception ex)
            {
                return new GetOperationResult { Success = false, ErrorMessage = ex.ToString() };
            }
        }

        private static GetOperationResult ParseResponse(string response, List<JToken> itemList)
        {
            JObject jobj = JObject.Parse(response);
            JArray jarr = (JArray)jobj["d"]["results"];

            itemList.AddRange(jarr);

            return new GetOperationResult
            {
                Success = true,
                Result = itemList,
                NextRequestUri = jobj["d"]["__next"]?.ToString()
            };
        }

        private class GetOperationResult : OperationResult<JToken>
        {
            public string NextRequestUri;
        }
    }

}

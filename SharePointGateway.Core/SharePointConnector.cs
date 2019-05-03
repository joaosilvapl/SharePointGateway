using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Newtonsoft.Json.Linq;

namespace SharePointGateway.Core
{
    public class SharePointConnector : ISharePointConnector
    {
        //TODO: add unit tests
        public OperationResult<ListItemDataProvider> GetListItems(DataSourceInfo dataSourceInfo)
        {
            var restQuery = new RestQueryBuilder().Build(dataSourceInfo.RestQueryData);

            var requestUri =
            $"{dataSourceInfo.SiteUri}{restQuery}";

            var allItems = new List<ListItemDataProvider>();

            //Retrieves items in batches
            do
            {
                var result = this.GetItems(dataSourceInfo.NetworkCredentials, requestUri);

                if (result.Result != null)
                {
                    allItems.AddRange(result.Result);
                }

                requestUri = result.NextRequestUri;

                if (allItems.Count >= dataSourceInfo.RestQueryData.MaxResults || string.IsNullOrWhiteSpace(requestUri))
                {
                    break;
                }

            } while (true);

            return new OperationResult<ListItemDataProvider> { Success = true, Result = allItems };
        }

        private GetListItemsOperationResult GetItems(NetworkCredential networkCredential, string requestUri)
        {
            HttpWebRequest endpointRequest = (HttpWebRequest)WebRequest.Create(requestUri);

            endpointRequest.Method = "GET";
            endpointRequest.Accept = "application/json;odata=verbose";

            endpointRequest.Credentials = networkCredential;

            try
            {
                var webResponse = endpointRequest.GetResponse();
                using (var webStream = webResponse.GetResponseStream())
                {
                    if (webStream == null)
                    {
                        throw new ApplicationException("webStream is null");
                    }

                    using (var responseReader = new StreamReader(webStream))
                    {
                        string response = responseReader.ReadToEnd();
                        JObject jobj = JObject.Parse(response);
                        JArray jarr = (JArray)jobj["d"]["results"];

                        var rawListItems = jarr.Select(x => new ListItemDataProvider(x));

                        var result = new GetListItemsOperationResult
                        {
                            Success = true,
                            Result = rawListItems,
                            NextRequestUri = jobj["d"]["__next"]?.ToString()
                        };


                        return result;

                    }
                }
            }
            catch (Exception ex)
            {
                return new GetListItemsOperationResult { Success = false, ErrorMessage = ex.ToString() };
            }
        }

        private class GetListItemsOperationResult : OperationResult<ListItemDataProvider>
        {
            public string NextRequestUri;
        }
    }
}

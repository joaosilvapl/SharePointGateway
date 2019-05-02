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
        public OperationResult<RawListItemData> GetListItems(DataSourceInfo dataSourceInfo)
        {
            var requestUri =
            $"{dataSourceInfo.SiteUri}/_api/web/lists/GetByTitle('{dataSourceInfo.ListTitle}')/items?$filter={dataSourceInfo.FilterQuery}&$select={dataSourceInfo.SelectQuery}&$orderby={dataSourceInfo.OrderBy}&$top={dataSourceInfo.MaxResults}";

            var allItems = new List<RawListItemData>();

            //Retrieves items in batches
            do
            {
                var result = this.GetItems(dataSourceInfo.NetworkCredentials, requestUri);

                allItems.AddRange(result.Result);

                requestUri = result.NextRequestUri;

                if (allItems.Count >= dataSourceInfo.MaxResults || string.IsNullOrWhiteSpace(requestUri))
                {
                    break;
                }

            } while (true);

            return new OperationResult<RawListItemData> { Success = true, Result = allItems };
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

                        var rawListItems = jarr.Select(x => new RawListItemData(x));

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

        private class GetListItemsOperationResult : OperationResult<RawListItemData>
        {
            public string NextRequestUri;
        }
    }
}

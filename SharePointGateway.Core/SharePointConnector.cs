using System.Linq;

namespace SharePointGateway.Core
{
    public class SharePointConnector : ISharePointConnector
    {
        private readonly IJsonWebClient _jsonWebClient;

        public SharePointConnector() : this(new JsonWebClient(new WebClient())) { }

        //Internal constructor for unit testing purposes
        internal SharePointConnector(IJsonWebClient jsonWebClient)
        {
            this._jsonWebClient = jsonWebClient;
        }

        //TODO: add unit tests
        public OperationResult<ListItemDataWrapper> GetListItems(DataSourceInfo dataSourceInfo)
        {
            var restQuery = new RestQueryBuilder().Build(dataSourceInfo.RestQueryData);

            var requestUri =
            $"{dataSourceInfo.SiteUri}{restQuery}";

            var getOperationResult = this._jsonWebClient.Get(requestUri, dataSourceInfo.NetworkCredentials,
                dataSourceInfo.RestQueryData.MaxResults);

            if (!getOperationResult.Success)
            {
                return new OperationResult<ListItemDataWrapper>
                {
                    Success = false,
                    ErrorMessage = getOperationResult.ErrorMessage
                };
            }

            var allItems = getOperationResult.Result.Select(x => new ListItemDataWrapper(x));

            return new OperationResult<ListItemDataWrapper> { Success = true, Result = allItems };
        }
    }
}

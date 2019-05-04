using System;
using System.Linq;

namespace SharePointGateway.Core
{
    public class ListItemRetriever
    {
        private readonly ISharePointConnector _sharePointConnector;

        public ListItemRetriever(ISharePointConnector sharePointConnector)
        {
            this._sharePointConnector = sharePointConnector;
        }

        public OperationResult<T> GetListItems<T>(DataSourceInfo dataSourceInfo, IListItemParser<T> listItemParser)
        {
            if (dataSourceInfo == null) throw new ArgumentNullException(nameof(dataSourceInfo));
            if (listItemParser == null) throw new ArgumentNullException(nameof(listItemParser));

            var operationResult = this._sharePointConnector.GetListItems(dataSourceInfo);

            if (!operationResult.Success)
            {
                return new OperationResult<T> {Success = false, ErrorMessage = operationResult.ErrorMessage};
            }

            var parsedListItems = operationResult.Result.Select(listItemParser.Parse);

            return new OperationResult<T> { Success = true, Result = parsedListItems };
        }
    }
}
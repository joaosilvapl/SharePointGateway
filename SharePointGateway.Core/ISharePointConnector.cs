using System.Collections.Generic;

namespace SharePointGateway.Core
{
    public interface ISharePointConnector
    {
        OperationResult<ListItemDataProvider> GetListItems(DataSourceInfo dataSourceInfo);
    }
}
using System.Collections.Generic;

namespace SharePointGateway.Core
{
    public interface ISharePointConnector
    {
        OperationResult<RawListItemData> GetListItems(DataSourceInfo dataSourceInfo);
    }
}
namespace SharePointGateway.Core
{
    public interface ISharePointConnector
    {
        OperationResult<ListItemDataWrapper> GetListItems(DataSourceInfo dataSourceInfo);
    }
}
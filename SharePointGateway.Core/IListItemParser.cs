namespace SharePointGateway.Core
{
    public interface IListItemParser<T> where T:new()
    {
        T Parse(IListItemDataProvider input);
    }
}
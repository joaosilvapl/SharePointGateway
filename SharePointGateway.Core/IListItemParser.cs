namespace SharePointGateway.Core
{
    public interface IListItemParser<T> where T:new()
    {
        T Parse(RawListItemData input);
    }
}
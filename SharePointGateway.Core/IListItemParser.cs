namespace SharePointGateway.Core
{
    public interface IListItemParser<T>
    {
        T Parse(IListItemDataWrapper input);
    }
}
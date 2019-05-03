namespace SharePointGateway.Core
{
    public interface IListItemDataProvider
    {
        object GetValue(string fieldName, string subFieldInternalName = null);
    }
}
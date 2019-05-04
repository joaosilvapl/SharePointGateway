namespace SharePointGateway.Core
{
    public interface IListItemDataWrapper
    {
        T GetValue<T>(string fieldName, string subFieldInternalName = null);
    }
}
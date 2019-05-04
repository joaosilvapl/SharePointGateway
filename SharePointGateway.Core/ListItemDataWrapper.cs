using Newtonsoft.Json.Linq;

namespace SharePointGateway.Core
{
    public class ListItemDataWrapper : IListItemDataWrapper
    {
        private readonly JToken _jToken;

        internal ListItemDataWrapper(JToken jToken)
        {
            _jToken = jToken;
        }

        public T GetValue<T>(string fieldName, string subFieldInternalName = null)
        {
            var value = this._jToken[fieldName];

            value = subFieldInternalName == null ? value : value?[subFieldInternalName];

            return value == null || value.Type == JTokenType.Null ? default(T) : value.Value<T>();
        }
    }
}

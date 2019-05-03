using Newtonsoft.Json.Linq;

namespace SharePointGateway.Core
{
    public class ListItemDataProvider : IListItemDataProvider
    {
        private readonly JToken _jToken;

        internal ListItemDataProvider(JToken jToken)
        {
            _jToken = jToken;
        }

        public object GetValue(string fieldName, string subFieldInternalName = null)
        {
            var value = this._jToken[fieldName];

            return subFieldInternalName == null ? value : value?[subFieldInternalName];
        }
    }
}

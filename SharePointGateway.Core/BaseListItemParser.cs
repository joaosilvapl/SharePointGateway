using System;

namespace SharePointGateway.Core
{
    public abstract class BaseListItemParser<T> : IListItemParser<T> where T:new()
    {
        public abstract T Parse(RawListItemData input);

        protected string GetFieldStringValue(RawListItemData input, string fieldInternalName)
        {
            return this.GetFieldValueOrDefault(input, fieldInternalName, x => x);
        }

        protected int GetFieldIntValue(RawListItemData input, string fieldInternalName)
        {
            return this.GetFieldValueOrDefault(input, fieldInternalName, int.Parse);
        }

        protected double GetFieldDoubleValue(RawListItemData input, string fieldInternalName)
        {
            return this.GetFieldValueOrDefault(input, fieldInternalName, double.Parse);
        }

        private TU GetFieldValueOrDefault<TU>(RawListItemData input, string fieldInternalName, Func<string, TU> convertFunction)
        {
            var textValue = this.GetFieldValue(input, fieldInternalName);

            if (string.IsNullOrWhiteSpace(textValue) || string.Compare(textValue, "null", StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                return default(TU);
            }

            return convertFunction(textValue);
        }

        private string GetFieldValue(RawListItemData input, string fieldInternalName)
        {
            var value = input.GetValue(fieldInternalName);
            return value?.ToString();
        }
    }
}
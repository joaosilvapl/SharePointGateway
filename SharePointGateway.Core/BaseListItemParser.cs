using System;

namespace SharePointGateway.Core
{
    public abstract class BaseListItemParser<T> : IListItemParser<T> where T:new()
    {
        public abstract T Parse(RawListItemData input);

        protected string GetFieldStringValue(RawListItemData input, string fieldInternalName, string subFieldInternalName = null)
        {
            return this.GetFieldValueOrDefault(input, fieldInternalName, subFieldInternalName, x => x);
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

            return FieldValueOrDefault(textValue, convertFunction);
        }

        private TU GetFieldValueOrDefault<TU>(RawListItemData input, string fieldInternalName, string subFieldInternalName, Func<string, TU> convertFunction)
        {
            var textValue = this.GetFieldValue(input, fieldInternalName, subFieldInternalName);

            return FieldValueOrDefault(textValue, convertFunction);
        }

        private static TU FieldValueOrDefault<TU>(string textValue, Func<string, TU> convertFunction)
        {
            if (string.IsNullOrWhiteSpace(textValue) ||
                string.Compare(textValue, "null", StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                return default(TU);
            }

            return convertFunction(textValue);
        }

        private string GetFieldValue(RawListItemData input, string fieldInternalName, string subFieldInternalName = null)
        {
            var value = input.GetValue(fieldInternalName, subFieldInternalName);
            return value?.ToString();
        }
    }
}
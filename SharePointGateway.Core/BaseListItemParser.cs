using System;

namespace SharePointGateway.Core
{
    public abstract class BaseListItemParser<T> : IListItemParser<T> where T:new()
    {
        public abstract T Parse(IListItemDataProvider input);

        protected string GetFieldStringValue(IListItemDataProvider input, string fieldInternalName, string subFieldInternalName = null)
        {
            return this.GetFieldValueOrDefault(input, fieldInternalName, subFieldInternalName, x => x);
        }

        protected int GetFieldIntValue(IListItemDataProvider input, string fieldInternalName, string subFieldInternalName = null)
        {
            return this.GetFieldValueOrDefault(input, fieldInternalName, subFieldInternalName, int.Parse);
        }

        protected double GetFieldDoubleValue(IListItemDataProvider input, string fieldInternalName, string subFieldInternalName = null)
        {
            return this.GetFieldValueOrDefault(input, fieldInternalName, subFieldInternalName, double.Parse);
        }

        private TU GetFieldValueOrDefault<TU>(IListItemDataProvider input, string fieldInternalName, string subFieldInternalName, Func<string, TU> convertFunction)
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

        private string GetFieldValue(IListItemDataProvider input, string fieldInternalName, string subFieldInternalName = null)
        {
            var value = input.GetValue(fieldInternalName, subFieldInternalName);
            return value?.ToString();
        }
    }
}
namespace SharePointGateway.Core
{
    public class BasicListItemParser : BaseListItemParser<BasicListItemData>
    {
        private const string IdFieldInternalName = "Id";
        private const string TitleFieldInternalName = "Title";

        public override BasicListItemData Parse(RawListItemData input)
        {
            return new BasicListItemData
            {
                ListItemId = this.GetFieldIntValue(input, IdFieldInternalName),
                Title = this.GetFieldStringValue(input, TitleFieldInternalName),
            };
        }
    }
}

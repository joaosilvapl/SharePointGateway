namespace SharePointGateway.Core
{
    public class BasicListItemParser : IListItemParser<BasicListItemData>
    {
        private const string IdFieldInternalName = "Id";
        private const string TitleFieldInternalName = "Title";

        public BasicListItemData Parse(IListItemDataWrapper input)
        {
            return new BasicListItemData
            {
                ListItemId = input.GetValue<int>(IdFieldInternalName),
                Title = input.GetValue<string>(TitleFieldInternalName)
            };
        }
    }
}

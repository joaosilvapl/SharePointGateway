using SharePointGateway.Core;

namespace SharePointGateway.TestApp
{
    class ListItemParser : BaseListItemParser<ListItemData>
    {
        private const string AssignedToFieldInternalName = "AssignedTo";
        private const string ModifiedFieldInternalName = "Modified";
        private const string NameFieldInternalName = "Name";

        private readonly BasicListItemParser _basicListItemParser = new BasicListItemParser();
        
        public override ListItemData Parse(IListItemDataProvider input)
        {
            var basicData = this._basicListItemParser.Parse(input);

            return new ListItemData
            {
                ListItemId = basicData.ListItemId,
                Title = basicData.Title,
                Modified = this.GetFieldStringValue(input, ModifiedFieldInternalName),
                AssignedToName = this.GetFieldStringValue(input, AssignedToFieldInternalName, NameFieldInternalName)
            };
        }
    }
}

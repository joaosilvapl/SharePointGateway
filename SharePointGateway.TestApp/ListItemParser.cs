using SharePointGateway.Core;

namespace SharePointGateway.TestApp
{
    class ListItemParser : IListItemParser<ListItemData>
    {
        private const string AssignedToFieldInternalName = "AssignedTo";
        private const string ModifiedFieldInternalName = "Modified";
        private const string NameFieldInternalName = "Name";
        private const string EstimateFieldInternalName = "Estimate";
        private const string TimeSpentFieldInternalName = "TimeSpent";

        private readonly BasicListItemParser _basicListItemParser = new BasicListItemParser();
        

        public ListItemData Parse(IListItemDataWrapper input)
        {
            var basicData = this._basicListItemParser.Parse(input);

            return new ListItemData
            {
                ListItemId = basicData.ListItemId,
                Title = basicData.Title,
                Modified = input.GetValue<string>(ModifiedFieldInternalName),
                AssignedToName = input.GetValue<string>(AssignedToFieldInternalName, NameFieldInternalName),
                Estimate = input.GetValue<double>(EstimateFieldInternalName),
                TimeSpent = input.GetValue<double>(TimeSpentFieldInternalName)
            };
        }
    }
}

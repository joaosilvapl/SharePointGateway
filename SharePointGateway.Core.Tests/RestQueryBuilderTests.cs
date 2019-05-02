using NUnit.Framework;

namespace SharePointGateway.Core.Tests
{
    [TestFixture]
    public class RestQueryBuilderTests
    {
        [Test]
        public void Build_test1()
        {
            var expectedQuery = "/_api/web/lists/GetByTitle('Cases')/items?$filter=ID eq 12833 or ID eq 18912&$select=Id,Title,TimeSpent,Modified&$orderby=Modified desc&$top=2000";

            var restQueryData = new RestQueryData
            {
                ListTitle = "Cases",
                FilterQuery = "ID eq 12833 or ID eq 18912",
                SelectQuery = "Id,Title,TimeSpent,Modified",
                OrderBy = "Modified desc",
                MaxResults = 2000,
            };

            var builder = this.GetBuilder();

            var result = builder.Build(restQueryData);

            Assert.That(result, Is.EqualTo(expectedQuery));
        }

        private RestQueryBuilder GetBuilder()
        {
            return new RestQueryBuilder();
        }
    }
}

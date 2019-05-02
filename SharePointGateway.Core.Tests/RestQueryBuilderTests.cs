using NUnit.Framework;

namespace SharePointGateway.Core.Tests
{
    [TestFixture]
    public class RestQueryBuilderTests
    {
        [Test]
        public void Build_if_expand_is_null_result_should_not_contain_expand_clause()
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

        [Test]
        public void Build_if_expand_is_not_null_result_should_contain_expand_clause()
        {
            var expectedQuery = "/_api/web/lists/GetByTitle('Cases')/items?$filter=ID eq 12833 or ID eq 18912&$select=Id,Title,TimeSpent,Modified&$orderby=Modified desc&$top=2000&$expand=AssignedTo";

            var restQueryData = new RestQueryData
            {
                ListTitle = "Cases",
                FilterQuery = "ID eq 12833 or ID eq 18912",
                SelectQuery = "Id,Title,TimeSpent,Modified",
                OrderBy = "Modified desc",
                MaxResults = 2000,
                Expand = "AssignedTo"
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

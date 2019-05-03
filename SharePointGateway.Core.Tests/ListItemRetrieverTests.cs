using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Rhino.Mocks;

namespace SharePointGateway.Core.Tests
{
    [TestFixture]
    public class ListItemRetrieverTests
    {
        [Test]
        public void GetListItems_if_sourceInfo_is_null_should_throw_argumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => this.GetListItemRetriever().GetListItems(null, MockRepository.GenerateStub<IListItemParser<object>>()));
        }

        [Test]
        public void GetListItems_if_listItemParser_is_null_should_throw_argumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
                this.GetListItemRetriever().GetListItems<object>(new DataSourceInfo(), null));
        }

        [Test]
        public void GetListItems_if_sharePointConnector_returns_error_should_return_errror()
        {
            const string expectedErrorMessage = "XYZ 123";

            DataSourceInfo dataSourceInfo = new DataSourceInfo();
            
            var fakeSharePointConnector = MockRepository.GenerateStub<ISharePointConnector>();
            fakeSharePointConnector.Stub(x => x.GetListItems(dataSourceInfo)).Return(
                new OperationResult<ListItemDataProvider> {Success = false, ErrorMessage = expectedErrorMessage});

            IListItemParser<object> fakeListItemParser = MockRepository.GenerateStub<IListItemParser<object>>();

            var listItemRetriever = this.GetListItemRetriever(fakeSharePointConnector);

            var result = listItemRetriever.GetListItems(dataSourceInfo, fakeListItemParser);

            Assert.That(result.Success, Is.False);
            Assert.That(result.ErrorMessage, Is.EqualTo(expectedErrorMessage));
        }


        [Test]
        public void GetListItems_if_sharePointConnector_returns_results_should_return_parsed_results()
        {
            DataSourceInfo dataSourceInfo = new DataSourceInfo();

            ListItemDataProvider fakeItem1 = new ListItemDataProvider(null);
            ListItemDataProvider fakeItem2 = new ListItemDataProvider(null);
            var fakeRawItems = new List<ListItemDataProvider>
            {
                fakeItem1,
                fakeItem2
            };

            var fakeSharePointConnector = MockRepository.GenerateStub<ISharePointConnector>();
            fakeSharePointConnector.Stub(x => x.GetListItems(dataSourceInfo)).Return(
                new OperationResult<ListItemDataProvider> { Success = true, Result = fakeRawItems});

            IListItemParser<object> fakeListItemParser = MockRepository.GenerateStub<IListItemParser<object>>();
            object fakeListItem1 = new object();
            object fakeListItem2 = new object();
            fakeListItemParser.Stub(x => x.Parse(fakeItem1)).Return(fakeListItem1);
            fakeListItemParser.Stub(x => x.Parse(fakeItem2)).Return(fakeListItem2);

            var listItemRetriever = this.GetListItemRetriever(fakeSharePointConnector);

            var result = listItemRetriever.GetListItems(dataSourceInfo, fakeListItemParser);

            Assert.That(result.Success, Is.True);
            var resultList = result.Result.ToList();
            Assert.That(resultList.Count, Is.EqualTo(2));
            Assert.That(resultList[0], Is.EqualTo(fakeListItem1));
            Assert.That(resultList[1], Is.EqualTo(fakeListItem2));
        }

        private ListItemRetriever GetListItemRetriever(ISharePointConnector sharePointConnector = null)
        {
            return new ListItemRetriever(sharePointConnector);
        }
    }
}

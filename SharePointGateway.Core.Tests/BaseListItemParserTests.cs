using System;
using NUnit.Framework;
using Rhino.Mocks;

namespace SharePointGateway.Core.Tests
{
    [TestFixture]
    public class BaseListItemParserTests
    {
        [Test]
        public void GetFieldStringValue_if_subFieldInternalName_is_null_should_return_main_value()
        {
            const string fieldName = "ABC";
            const string fieldValue = "4455";
            const string expectedResult = fieldValue;

            var parser = this.GetParser();

            var fakeItemData = MockRepository.GenerateStub<IListItemDataProvider>();
            fakeItemData.Stub(x => x.GetValue(fieldName)).Return(fieldValue);

            var result = parser.GetStringValue(fakeItemData, fieldName);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void GetFieldStringValue_if_subFieldInternalName_is_not_null_should_return_sub_value()
        {
            const string fieldName = "ABC";
            const string subFieldName = "UUYE";
            const string fieldValue = "4455";
            const string expectedResult = fieldValue;

            var parser = this.GetParser();

            var fakeItemData = MockRepository.GenerateStub<IListItemDataProvider>();
            fakeItemData.Stub(x => x.GetValue(fieldName, subFieldName)).Return(fieldValue);

            var result = parser.GetStringValue(fakeItemData, fieldName, subFieldName);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void GetFieldIntValue_if_subFieldInternalName_is_null_should_return_main_value()
        {
            const string fieldName = "ABC";
            const string fieldValue = "4455";
            const int expectedResult = 4455;
            
            var parser = this.GetParser();

            var fakeItemData = MockRepository.GenerateStub<IListItemDataProvider>();
            fakeItemData.Stub(x => x.GetValue(fieldName)).Return(fieldValue);

            var result = parser.GetIntValue(fakeItemData, fieldName);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void GetFieldIntValue_if_subFieldInternalName_is_not_null_should_return_sub_value()
        {
            const string fieldName = "ABC";
            const string subFieldName = "UUYE";
            const string fieldValue = "4455";
            const int expectedResult = 4455;

            var parser = this.GetParser();

            var fakeItemData = MockRepository.GenerateStub<IListItemDataProvider>();
            fakeItemData.Stub(x => x.GetValue(fieldName, subFieldName)).Return(fieldValue);

            var result = parser.GetIntValue(fakeItemData, fieldName, subFieldName);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        public TestListItemParser GetParser()
        {
            return new TestListItemParser();
        }
    }

    public class TestListItemParser : BaseListItemParser<BasicListItemData>
    {
        public override BasicListItemData Parse(IListItemDataProvider input)
        {
            throw new NotImplementedException();
        }

        public string GetStringValue(IListItemDataProvider input, string fieldInternalName, string subFieldInternalName = null)
        {
            return this.GetFieldStringValue(input, fieldInternalName, subFieldInternalName);
        }

        public int GetIntValue(IListItemDataProvider input, string fieldInternalName, string subFieldInternalName = null)
        {
            return this.GetFieldIntValue(input, fieldInternalName, subFieldInternalName);
        }

        public double GetDoubleValue(IListItemDataProvider input, string fieldInternalName, string subFieldInternalName = null)
        {
            return this.GetFieldDoubleValue(input, fieldInternalName, subFieldInternalName);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Xunit;
using Station.Domain;

namespace DomainTests
{
    public class NameTests
    {
        [Fact]
        public void Name_HasDefaultValue_WhenCreated_WithNull()
        {
            var name = new Name(null);
            Assert.Equal( "Default Station", name.value );
        }
        
        [Theory]
        [InlineData("", "Default Station")]
        [InlineData("     ", "Default Station")]
        public void Name_HasDefaultValue_WhenCreated_WithNoName(string actual, string expected)
        {
            var name = new Name(actual);
            Assert.Equal( name.value , expected);
        }
        
        [Theory]
        [InlineData("Akhnaton  ", "Akhnaton")] // trim
        [InlineData("Akhnaton", "Akhnaton")]   // assign
        [InlineData("Akhnaton Station left of Huntington", "Akhnaton Station left of")]   // cut to maxlength
        public void Name_IsTrimmedAndAssigned_WhenGiven(string actual, string expected)
        {
            var name = new Name(actual);
            Assert.Equal( name.value , expected);
        }
    }

    public class DescriptionTests
    {

        
        [Fact]
        public void Description_HasDefaultValue_WhenCreated_WithNull()
        {
            var description = new Description(null);
            Assert.Equal( "No description", description.value );
        }
        
        [Theory]
        [ClassData(typeof(DescriptionTestData))]
        public void Description_IsTrimmedAndAssigned_WhenGiven(string actual, string expected)
        {
            var description = new Description(actual);
            Assert.Equal( description.value , expected);
        }
        
        // Testdata class for description tests
        private class DescriptionTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { null, "No description" };
                yield return new object[] { "", "No description" };
                yield return new object[] { "        ",   "No description" };
                yield return new object[] { "Description  ", "Description" };
                yield return new object[] { "Description",   "Description" };
                yield return new object[] { new string('*', 300), new string('*', 255 ) };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
   
}
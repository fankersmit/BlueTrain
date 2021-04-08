using System;
using Xunit;
using BlueTrain.Terminal;
using BlueTrain.Shared;
using NuGet.Frameworks;
using Xunit.Sdk;


namespace BlueTrainTests
{
    public class TerminalInformationTests
    {
        // fields
        readonly Uri _uri;
        readonly Guid _ID;

        // ctor
        public TerminalInformationTests()
        {
            _uri = new Uri("http://www.eaxample.com");
            _ID = Guid.Parse("B27DAA81-18B9-4030-844B-7E7C93727580");
        }

        [Theory]
        [InlineData(  false , true, "Name", "Description", "Address")]
        [InlineData(  true , false, "Name", "Description",  "ID")]
        [InlineData(  true , true, null, "Description",  "Name")]
        [InlineData(  true , true, "Name", null,  "Description")]
        [InlineData(  true , true, "", "Description",  "Name")]
        [InlineData(  true , true, "Name", "", "Description")]
        public void Create_Fails_With_Exception_On_Fawlty_Parameters(
            bool addUri,bool id, string n, string d, string  argument)
        {
            // arrange
            Uri address = addUri ? _uri : null;
            Guid  Id = id ? _ID : Guid.Empty ;
            var status = TerminalStatus.Closed;
            var message = $"{argument}: Does not  have a non empty, not null and valid value";
            // act
            var ex = Assert.Throws<ArgumentException>(
                () => TerminalInformation.Create(address, Id, n, d, status));
            // assert
            ex.Message.Contains(argument);
        }

        [Fact]
        public void Create_Fills_All_Fields()
        {
            var  _terminalUri = _uri;
            var _name = "Basic Terminal";
            var _description = "Basic Terminal with no capabilities";
            Guid _Id = _ID;
            TerminalStatus _status = TerminalStatus.Closed;

            // arrange

            // act, Assert
            var ci = TerminalInformation.Create(_terminalUri,_ID,_name,_description,_status);
            var now = DateTime.UtcNow;
            var status = Enum.GetName(_status);

            // assert
            Assert.Equal(_terminalUri, ci.Address);
            Assert.Equal(_name, ci.Name);
            Assert.Equal(_description, ci.Description);
            Assert.Equal(_Id, ci.ID);
            Assert.Equal(status, ci.Status);
            Assert.True( ci.InformationTimeStamp <= now);
        }
    }
}

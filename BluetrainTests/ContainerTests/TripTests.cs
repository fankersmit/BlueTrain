using System;
using BlueTrain.Containers;
using Xunit;
using BlueTrain.Terminal;
using BlueTrain.Shared;


namespace BlueTrainTests
{
    public class TripTests
    {
        [Theory]
        [InlineData(true,  false, true)]
        [InlineData(false, false, true)]
        [InlineData(false, true,  true)]
        [InlineData(true, true , false)]
        public void Null_Departure_And_Destination_Throws_ArgumentExceptionITerminal( bool t1, bool t2, bool throwsException )
        {
            // arrange
            var ti = CreateDepartureAndDestination();
            ITerminalInformation ti1 = t1 ? ti.Item1 : null;
            ITerminalInformation ti2 = t2 ? ti.Item2 : null;
            var expectedMessage = "Departure or destination cannot be null.";

            // act
            if (throwsException)
            {
                var ex = Assert.Throws<ArgumentException>(() => new Trip(ti1, ti2));
                // assert
                Assert.Equal( expectedMessage,ex.Message);
            }
        }

        [Fact]
        public void Null_Destination_Throws_ArgumentExceptionITerminal()
        {
            // arrange
            ITerminalInformation destinationTerminal = null;
            var expectedMessage = "Destination cannot be null.";
            // act
            var ex = Assert.Throws<ArgumentException>(() => new Trip(destinationTerminal));
            // assert
            Assert.Equal(expectedMessage, ex.Message);
        }

        [Fact]
        public void Equal_Departure_And_Destination_Throws_ArgumentException()
        {
            // arrange
            var ti = CreateDepartureAndDestination();
            var expectedMessage = "Departure and destination cannot be the same.";

            // act, use departure also as destination
            var ex = Assert.Throws<ArgumentException>(() => new Trip(ti.Item1, ti.Item1));
            // assert
            Assert.Equal( expectedMessage,ex.Message);
        }

        [Fact]
        public void Depart_Sets_DepartedOn()
        {
            // arrange
            var now = DateTime.UtcNow;
            var trip = CreateValidTrip();
            // act
            trip.Depart();
            // assert
            Assert.True(now < trip.DepartedOn);
        }

        [Fact]
        public void Departing_After_Arriving_Throws_InvalidOperationException()
        {
            // arrange
            var message = "Exception: You cannot depart after leaving, or trip is done.";
            var trip = CreateValidTrip();
            trip.Depart();
            trip.Arrive();

            // act
            var  ex = Assert.Throws<InvalidOperationException>( () => trip.Depart() );
            // assert
            Assert.Equal(message, ex.Message);
        }


        [Fact]
        public void Arriving_Before_Departing_Throws_InvalidOperationException()
        {
            // arrange
            var message = "Exception: You cannot arrive before leaving, call Depart() first.";
            var trip = CreateValidTrip();
            // act
            var  ex = Assert.Throws<InvalidOperationException>( () => trip.Arrive() );
            // assert
            Assert.Equal(message, ex.Message);
        }

        [Fact]
        public void Arrive_Sets_ArrivedOn()
        {
            // arrange
            var now = DateTime.UtcNow;
            var trip = CreateValidTrip();
            // act
            trip.Depart();
            trip.Arrive();
            // assert
            Assert.True(now < trip.ArrivedOn);
        }

        [Fact]
        public void Arrive_Sets_IsDone()
        {
            // arrange
            var now = DateTime.UtcNow;
            var trip = CreateValidTrip();
            // act
            trip.Depart();
            trip.Arrive();
            // assert
            Assert.True(trip.IsDone);
        }

        [Theory]
        [InlineData(true, true)]
        [InlineData(false, true)]
        public void New_Trip_Always_Has_End(bool hasStart, bool hasEnd )
        {
            // arrange,  act
            var trip = CreateValidTrip(hasStart);

            // assert
            Assert.Equal( hasStart, trip.DepartureTerminal != null );
            Assert.Equal (hasEnd, trip.DestinationTerminal != null );
        }

        [Fact]
        public void New_Trip_Can_Have_Null_Start()
        {
            // arrange,  act
            var destinationTerminal = CreateDepartureAndDestination().Item2;
            var trip =  new Trip(destinationTerminal);

            // assert
            Assert.Null(trip.DepartureTerminal);
            Assert.NotNull(trip.DestinationTerminal);
        }


        [Fact]
        public void New_Default_Trip_Has_Start_And_End()
        {
            // arrange,  act
            var trip = CreateValidTrip();

            // assert
            Assert.NotNull(trip.DepartureTerminal);
            Assert.NotNull(trip.DestinationTerminal);
        }

        [Fact]
        public void New_Trip_Times_Are_MinValue()
        {
            // arrange,  act
            var trip = CreateValidTrip();
            // assert
            Assert.Equal(DateTime.MinValue, trip.DepartedOn);
            Assert.Equal(DateTime.MinValue, trip.ArrivedOn);
        }

        [Fact]
        public void New_Trip_Is_NotDone()
        {
            // arrange
            var trip = CreateValidTrip();
            // act
            // assert
            Assert.False(trip.IsDone);
        }

        // private helper methods
        Tuple<TerminalInformation, TerminalInformation> CreateDepartureAndDestination()
        {
            var uri = new Uri("http://example.com/api");
            var name = "terminalName";
            var description = "terminalDescription";
            var id_01 = Guid.NewGuid();
            var id_02 = Guid.NewGuid();

            var departure = TerminalInformation.Create(uri, id_01, name, description, TerminalStatus.Open);
            var destination = TerminalInformation.Create(uri, id_02, name, description, TerminalStatus.Open);
            return new Tuple<TerminalInformation, TerminalInformation>( departure, destination);
        }

        private Trip CreateValidTrip( bool hasStartingTerminal = true)
        {
            var ti = CreateDepartureAndDestination();
            return hasStartingTerminal ? new Trip(ti.Item1, ti.Item2) : new Trip(ti.Item2);
        }
    }
}

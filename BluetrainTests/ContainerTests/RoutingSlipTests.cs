using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using BlueTrain.Containers;
using Xunit;
using BlueTrain.Terminal;
using BlueTrain.Shared;

namespace BlueTrainTests
{
    public class RoutingSlipTests
    {
        [Fact]
        public void New_RoutingSlip_Is_Initialized()
        {
            // arrange
            var now = DateTime.UtcNow;
            var container = CreateContainer();
            var containerInfo = container.Information();
            var expectedTrips = 0;

            // act
            var routingSlip = new RoutingSlip(containerInfo);
            // assert
            Assert.True(now < routingSlip.CreatedOn);
            Assert.Equal(expectedTrips, routingSlip.Trips.Count);
            Assert.Equal(containerInfo, routingSlip.ContainerInformation);
        }

        [Fact]
        public void Cannot_Add_Same_Trip_Again_Throws_ArgumentException()
        {
            // arrange
            var pattern  = new Regex("Trip with ID: \\([0-9a-z-]+\\) already in journey: Cannot add trip\\.");

            var rs = CreateRoutingSlip();
            var trip = CreateValidTrip();

            // act
            rs.Add(trip);
            var expected = rs.Trips.Count;
            var ex = Assert.Throws<ArgumentException>(() => rs.Add(trip));
            var actual = rs.Trips.Count;

            // assert
            Assert.True(rs.Trips.Contains(trip));
            Assert.Equal(expected, actual);
            Assert.Matches(pattern, ex.Message);
        }

        [Fact]
        public void Can_Add_Trip()
        {
            // arrange
            var rs = CreateRoutingSlip();
            var trip = CreateValidTrip();

            // act
            var initialTripCount = rs.Trips.Count;
            rs.Add(trip);

            // assert
            Assert.Equal(initialTripCount + 1, rs.Trips.Count);
            Assert.True(rs.Trips.Contains(trip));
        }

        [Fact]
        public void Can_Add_Trip_Collection()
        {
            // arrange
            var rs = CreateRoutingSlip();
            var trip1 = CreateValidTrip();
            var trip2 = CreateValidTrip();
            IEnumerable<Trip> trips = new List<Trip>() {trip1, trip2};

            // act
            var initialTripCount = rs.Trips.Count;
            rs.Add(trips);

            // assert
            Assert.Equal(initialTripCount + trips.LongCount(), rs.Trips.Count);
            Assert.True(rs.Trips.Contains(trip1));
            Assert.True(rs.Trips.Contains(trip2));
        }


        // private helper methods
        private RoutingSlip CreateRoutingSlip()
        {
            var containerInfo = CreateContainer().Information();
            return new RoutingSlip(containerInfo);
        }

        private Container CreateContainer()
        {
            return new Container(Guid.NewGuid(), "Name", "Description");
        }

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

        Trip CreateValidTrip()
        {
            var ti = CreateDepartureAndDestination();
            return new Trip( ti.Item1, ti.Item2);
        }
    }
}

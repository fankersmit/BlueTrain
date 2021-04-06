using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BlueTrain.Shared;

namespace BlueTrain.Containers
{
    public class RoutingSlip
    {
        // fields
        private List<Trip> _trips;
        private readonly DateTime _createdOn;
        private readonly ContainerInformation _containerInformation;

        // properties
        public ContainerInformation ContainerInformation => _containerInformation;
        public IList<Trip> Trips => _trips;
        public DateTime CreatedOn => _createdOn;

        // ctors
        public RoutingSlip(ContainerInformation containerInfo)
        {
            _trips = new List<Trip>();
            _containerInformation = containerInfo;
            _createdOn = DateTime.UtcNow;
        }

        // methods
        public void Add(Trip nextTrip)
        {
            // throws argument exception
            ValidateTripNotInJourney( nextTrip);
            _trips.Add(nextTrip);
        }

        public void Add(IEnumerable<Trip> nextTrips)
        {
            foreach (var trip in nextTrips)
            {
                _trips.Add(trip);
            }
        }

        // get the next destination to send container
        // if none is found, returns null
        public ITerminalInformation GetNextDestination()
        {
            ITerminalInformation terminalInformation = null;
            // loop through trips
            foreach (var trip in _trips)
            {
                if (!trip.IsDone)
                {
                    terminalInformation = trip.DestinationTerminal;
                    break;
                }
            }
            return terminalInformation;
        }

        // private helper methods
        private void ValidateTripNotInJourney(Trip nextTrip)
        {
            var trip = Trips.FirstOrDefault( t => t.ID == nextTrip.ID);
            if (trip != null)
            {
                var message  = $"Trip with ID: ({nextTrip.ID}) already in journey: Cannot add trip.";
                throw new ArgumentException(message);
            }
        }
    }
}

using System;
using Shared;

namespace BlueTrain.Containers
{
    public struct Trip
    {
        // fields
        public readonly ITerminalInformation DepartureTerminal;
        public readonly ITerminalInformation DestinationTerminal;

        // properties
        public bool Done { get; set; }
        public DateTime Departure { get; set;  }
        public DateTime Arrival { get; set;  }

        // ctors
        public Trip(ITerminalInformation departureTerminal, ITerminalInformation destinationTerminal)
        {
            DepartureTerminal = departureTerminal;
            DestinationTerminal = destinationTerminal;
            Done = false;
        }
    }
}

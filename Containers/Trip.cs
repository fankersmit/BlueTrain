using System;
using BlueTrain.Shared;

namespace BlueTrain.Containers
{
    public class Trip
    {
        // fields
        public readonly ITerminalInformation DepartureTerminal;
        public readonly ITerminalInformation DestinationTerminal;

        // properties
        public bool IsDone { get; private set; }
        public DateTime DepartedOn { get; set;  }
        public DateTime ArrivedOn { get; set;  }

        // ctors
        public Trip(ITerminalInformation departureTerminal, ITerminalInformation destinationTerminal)
        {
            // throws argumnent exception on failure
            ValidateArguments(departureTerminal, destinationTerminal); 

            DepartureTerminal = departureTerminal;
            DestinationTerminal = destinationTerminal;
            IsDone = false;
        }

        private void ValidateArguments(ITerminalInformation departure, ITerminalInformation destination)
        {
            string message = string.Empty;

            if (departure == null | destination == null)
            {
                message = "Departure or destination cannot be null.";
            } else if (departure.ID == destination.ID)
            {
                message = "Departure and destination cannot be the same.";
            }

            if (!string.IsNullOrEmpty(message))
            {
                throw new ArgumentException(message);
            }
        }

        public void Depart()
        {
            DepartedOn = DateTime.UtcNow;
        }

        public void Arrive()
        {
            if (DepartedOn == DateTime.MinValue)
            {
                var message = "Exception: You cannot arrive before leaving, call Depart() first.";
                throw new InvalidOperationException(message);
            }
            ArrivedOn = DateTime.UtcNow;
            IsDone = true;
        }
    }
}

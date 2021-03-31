using System;
using BlueTrain.Shared;

namespace BlueTrain.Containers
{
    public class Trip
    {
        // fields
        public readonly ITerminalInformation DestinationTerminal;
        private readonly Guid _Id;

        // properties
        public ITerminalInformation DepartureTerminal { get; set; }
        public Guid ID => _Id;
        public bool IsDone { get; private set; }
        public DateTime DepartedOn { get; set;  }
        public DateTime ArrivedOn { get; set;  }

        // ctors
        public Trip( ITerminalInformation destinationTerminal)
        {
            // throws argument exception on failure
            ValidateArguments( destinationTerminal );

            DestinationTerminal = destinationTerminal;
            _Id = Guid.NewGuid();
            IsDone = false;
        }

        public Trip(ITerminalInformation departureTerminal, ITerminalInformation destinationTerminal)
        {
            // throws argument exception on failure
            ValidateArguments(departureTerminal, destinationTerminal); 

            DepartureTerminal = departureTerminal;
            DestinationTerminal = destinationTerminal;
            _Id = Guid.NewGuid();
            IsDone = false;
        }

        private void ValidateArguments( ITerminalInformation destination)
        {
            string message = string.Empty;

            if (destination == null)
            {
                message = "Destination cannot be null.";
                throw new ArgumentException(message);
            }
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
            if (ArrivedOn != DateTime.MinValue)
            {
                var message = "Exception: You cannot depart after leaving.";
                throw new InvalidOperationException(message);
            }
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

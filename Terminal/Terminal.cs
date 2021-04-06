using System;
using System.Collections.Generic;
using System.Net.Http;
using BlueTrain.Containers;
using BlueTrain.Shared;

namespace BlueTrain.Terminal
{
    public class Terminal : ITerminal
    {
        // ctor
        public Terminal(Uri address, string name, string description, Guid identifier)
        {
            Address = address;
            Name = name;
            Description = description;
            Id = identifier;
            Status = TerminalStatus.Closed;
            Capabilities = new Dictionary<string, string>();

            // handling of containers
            HoldingYard = new HoldingYard();
            HasValidRoutingSlip = false;
        }

        // properties
        public Uri Address { get; }
        public Guid Id { get; }
        public string Name { get; }
        public string Description { get; }
        public Dictionary<string, string> Capabilities { get; }
        public TerminalStatus Status { get; protected set; }
        public IHoldingYard HoldingYard { get; }
        public bool HasValidRoutingSlip { get; private set; }

        // methods
        public bool IsClosed()
        {
            return (Status == TerminalStatus.Closed);
        }

        public bool IsOpen()
        {
            return (Status == TerminalStatus.Open);
        }

        public void Open()
        {
            if( Status != TerminalStatus.Closed)
                return;

            Status = TerminalStatus.Opening;
            // do stuff
            // on success
            Status = TerminalStatus.Open;
        }

        public void Close()
        {
            if( Status != TerminalStatus.Open)
                return;

            Status = TerminalStatus.Closing;
            // do stuff
            // on success
            Status = TerminalStatus.Closed;
        }

        public TerminalInformation GetTerminalInfo()
        {
            return TerminalInformation.Create(Address, Id, Name, Description, Status);
        }

        public void Receive(Container container)
        {
            HoldingYard.Add(container);
        }

        // send container to the next terminal belonging to trip
        public void Send(Container container )
        {
            // cargo can be changed through processing
            var ctr = HoldingYard.Find(container);

            // throws invalid operation exception
            ValidateStatusAndContainer(ctr);

            var nextTerminal = container.RoutingSlip.GetNextDestination();
            HoldingYard.Remove(container);
            //nextTerminal.Receive( );
        }

        private void ValidateStatusAndContainer(Container container)
        {
            var errorCount = 0;
            var message = string.Empty;

            // only send if terminal is open and in service
            if (Status != TerminalStatus.Open)
            {
                message += $"({++errorCount}) Terminal is closed: Cannot send Container.";
            }

            if (container == null)
            {
                message = $" ({++errorCount}) Container unknown: Container not in holding yard.";
            }
            else if (!container.HasRoutingSlip)
            {
                message += $" ({++errorCount}) Destination unknown: Container has no routing slip.";
            }

            if (errorCount > 0)
            {
                throw new InvalidOperationException(message);
            }
        }

        // send container to a specific terminal, ignoring routing slip
        public void Send(Container container, Terminal nextTerminal)
        {
            // cargo can be changed through processing
            var ctr = HoldingYard.Find(container);
            ValidateStatusAndContainer(ctr);

            HoldingYard.Remove(container);
            nextTerminal.Receive(ctr);
        }

        public void Remove(ContainerInformation containerInfo)
        {
            var ctr = HoldingYard.FindByInfo(containerInfo);
            if (ctr != null)
            {
                HoldingYard.Remove(ctr);
            }
        }
    }
}

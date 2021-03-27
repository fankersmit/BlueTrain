using System;
using System.Linq;
using System.Collections.Generic;
using BlueTrain.Containers;
using BlueTrain.Shared;

namespace BlueTrain.Terminal
{
    public class Terminal : ITerminal
    {
        // ctor
        public Terminal(Uri address, string name, string description, Guid Identifier)
        {
            Address = address;
            Name = name;
            Description = description;
            Id = Identifier;
            Status = TerminalStatus.Closed;
            Capabilities = new Dictionary<string, string>();

            // handling of containers
            HoldingYard = new HoldingYard();
        }

        // properties
        public Uri Address { get; }
        public Guid Id { get; }
        public string Name { get; }
        public string Description { get; }
        public Dictionary<string, string> Capabilities { get; }
        public TerminalStatus Status { get; protected set; }
        public IHoldingYard HoldingYard { get; }

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
            return TerminalInformation.Create(null, Id, Name, Description, Status);
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
            if (ctr == null)
            {
                var message = "Container unknown: Container not in holding yard.";
                throw new InvalidOperationException(message);
            }
            if (!ctr.HasRoutingSlip)
            {
                var message = "Destination unknown: Container has no routing slip,";
                throw new InvalidOperationException(message);
            }
            var nextTerminal = container.RoutingSlip.GetNextDestination();
            HoldingYard.Remove(container);
            //nextTerminal.Receive( );
        }

        // send container to a specific terminal, ignoring routing slip
        public void Send(Container container, Terminal nextTerminal)
        {
            // cargo can be changed through processing
            var ctr = HoldingYard.Find(container);
            if (ctr == null)
            {
                var message = "Container unknown: Container not in holding yard.";
                throw new InvalidOperationException(message);
            }
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

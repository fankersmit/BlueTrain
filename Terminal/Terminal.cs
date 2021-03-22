using System;
using System.Collections;
using System.Collections.Generic;
using BlueTrain.Shared;

namespace BlueTrain.Terminal
{
    public class Terminal : ITerminal
    {
        // ctor
        public Terminal(string name, string description, Guid Identifier)
        {
            Name = name;
            Description = description;
            Id = Identifier;
            Status = TerminalStatus.Closed;
            Capabilities = new Dictionary<string, string>();

            // handling of containers
            HoldingYard = new HoldingYard();
        }

        // properties
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
            return new TerminalInformation
            {
                Name = this.Name,
                Description = this.Description,
                ID = this.Id.ToString(),
                Status = Enum.GetName(this.Status)
            };
        }
    }
}

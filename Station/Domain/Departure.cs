using System;
using Terminal.Domain;

namespace Terminal.Domain
{
    public class Departure
    {
        public IContainerInfo Container { get; }
        public DateTime ArrivedAt { get; }
        public ITerminalInfo DepartedTo { get;  }
    }
}
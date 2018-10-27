using System;
using Terminal.Domain;

namespace Terminal.Domain
{
    public class Arrival
    {
        public Arrival(Container container, BaseTerminal departedFromTerminal)
        {
            Container = container;
            DepartedFrom = departedFromTerminal as ITerminalInfo;
            ArrivedAt = DateTime.Now.ToUniversalTime();
        }

        public IContainerInfo Container { get;  }
        public DateTime ArrivedAt { get; }
        public ITerminalInfo DepartedFrom { get;  }
    }
}
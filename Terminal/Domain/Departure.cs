using System;
using Terminal.Domain;

namespace Terminal.Domain
{
    public class Departure
    {
        public Departure( Container container  )
        {
            Container = container;
            DepartedAt = DateTime.Now.ToUniversalTime();
        }
        
        // properties
        public IContainerInfo Container { get; }
        public DateTime DepartedAt { get; }
        public ITerminalInfo DepartedTo { get;  }
    }
}
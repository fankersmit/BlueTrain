using System;
using System.Collections.Generic;

namespace Terminal.Domain
{
    public interface ITerminalInfo
    {
        Guid Id { get; }
        Name Name { get; }
        Description Description { get;  }
        Dictionary<string, string> Capabilities { get;  }
        TerminalStatus Status { get; set; }
    }
}
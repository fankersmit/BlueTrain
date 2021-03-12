using System;
using System.Collections.Generic;
using BlueTrain.Shared;

namespace BlueTrain.Terminal
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

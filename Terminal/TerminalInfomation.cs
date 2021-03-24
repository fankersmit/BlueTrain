using System;
using Shared;

namespace BlueTrain.Terminal
{
    public readonly struct TerminalInformation : ITerminalInformation
    {
        // fields
        public readonly string Name { get;  }
        public readonly string Description{ get;  }
        public readonly string ID{ get;  }
        public readonly string Status{ get;  }

        public TerminalInformation(string id, string name, string description, string status)
        {
            Name = name;
            Description = description;
            ID = id;
            Status = status;
        }
    }
}

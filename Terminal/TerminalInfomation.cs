using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using BlueTrain.Shared;

namespace BlueTrain.Terminal
{
    public readonly struct TerminalInformation : ITerminalInformation
    {
        // fields
        public readonly Uri Address { get;  }
        public readonly string Name { get;  }
        public readonly string Description{ get;  }
        public readonly Guid ID{ get;  }
        public readonly TerminalStatus Status{ get;  }
        public readonly DateTime TimeStamp;

        private TerminalInformation(Uri address, Guid id, string name, string description, TerminalStatus status)
        {
            Address = address;
            Name = name;
            Description = description;
            ID = id;
            Status = status;
            TimeStamp = DateTime.UtcNow;
        }

        public string ToJsonString()
        {
            return JsonSerializer.Serialize<TerminalInformation>(this);
        }

        // should only be called from  within a terminal
        public static TerminalInformation Create(Uri terminalUri, Guid id, string name, string description, TerminalStatus status)
        {
            // throws ArgumentException if any one of the arguments is invalid
            ValidateArguments(terminalUri, id, name, description, status);
            return new TerminalInformation(terminalUri, id, name, description, status);
        }

        private static void ValidateArguments(Uri terminalUri, Guid id, string name, string description, TerminalStatus status)
        {
            string[] argumentNames = {"Address", "ID", "Name", "Description"};
            int index = -1;

            if (terminalUri == null) {
                index = 0;          // uri
            } else if (id == Guid.Empty) {
                index = 1;          //id
            } else if (string.IsNullOrEmpty(name))  {
                index = 2;          // name
            } else if( string.IsNullOrEmpty(description)) {
                index = 3;          // description
            }

            if (index >= 0 )
            {
                var message = $"{argumentNames[index]}: Does not have a non empty, non null or valid value";
                throw new ArgumentException(message);
            }
        }
    }
}

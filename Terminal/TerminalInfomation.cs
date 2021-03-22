using System;

namespace BlueTrain.Terminal
{
    public class TerminalInformation
    {
        // fields
        public string Name { get; set; }
        public string Description{ get; set; }
        public string ID{ get; set; }
        public string Status{ get; set; }

        // ctor
        /*
        internal TerminalInformation(string name, string description, Guid Id, TerminalStatus status)
        {
            Name = name;
            Description = description;
            ID = Id.ToString();
            Status = Enum.GetName(status);
        }
        */
    }
}

using System;


namespace BlueTrain.Shared
{
    public interface ITerminalInformation
    {
        public Uri Address { get; }
        public string Name { get; }
        public string Description { get; }
        public Guid ID { get; }
        public string Status { get; }
        public DateTime InformationTimeStamp { get; }
    }
}

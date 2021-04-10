using System;

namespace BlueTrain.Terminal
{
    public interface IHoldingYardInformation
    {
        public int Capacity { get; }
        public bool IsEmpty { get; }
        public bool IsFilled { get; }
        public int Count { get; }
        public DateTime InformationTimeStamp { get; }
    }
}

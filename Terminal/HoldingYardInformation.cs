using System;

namespace BlueTrain.Terminal
{
    public class HoldingYardInformation :IHoldingYardInformation
    {
        // ctor
        public HoldingYardInformation(int capacity, int count)
        {
            Capacity = capacity;
            Count = count;
            IsEmpty = (count == 0);
            IsFilled = (Count >= Capacity);
            InformationTimeStamp = DateTime.UtcNow;
        }

        public int Capacity { get; private set; }
        public bool IsEmpty { get; private set; }
        public bool IsFilled { get; private set; }
        public int Count { get; private set; }
        public DateTime InformationTimeStamp { get; private set; }
    }
}

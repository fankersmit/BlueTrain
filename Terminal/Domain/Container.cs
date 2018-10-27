using System;

namespace Terminal.Domain
{
    public class Container : IContainerInfo
    {
        public Guid Id { get; }
        public Name Name { get; }
        public Description Description { get; }
        public ContainerStatus Status { get; set; }

        public Container(Name n, Description d)
        {
            Name = n;
            Description = d;
            Id  = Guid.NewGuid();
            Status = ContainerStatus.Empty;
       }
    }
}
using System;
using BlueTrain.Shared;

namespace BlueTrain.Containers
{
    public class Container : IContainerInfo
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Description { get; }
        public ContainerStatus Status { get; set; }

        public Container(string n, string d)
        {
            Name = n;
            Description = d;
            Id  = Guid.NewGuid();
            Status = ContainerStatus.Empty;
       }
    }
}

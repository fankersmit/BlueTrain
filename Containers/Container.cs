using System;
using BlueTrain.Shared;


namespace BlueTrain.Containers
{
    public class Container
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Description { get; }
        public ContainerStatus Status { get; set; }
        public RoutingSlip RoutingSlip { get; set; }

        public Container(Guid ID, string name , string description )
        {
            Name = name;
            Description = description;
            Id = ID;
            Status = ContainerStatus.Empty;
            RoutingSlip = null;
        }

        public ContainerInformation Information()
        {
            return new (Id, Name, Description);
        }
    }
}

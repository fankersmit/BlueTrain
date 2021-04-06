using System;
using System.Text.Json;
using System.Text.Json.Serialization;
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
        public DateTime CreatedOn { get; }

        public bool HasRoutingSlip => RoutingSlip != null;

        [JsonConstructor]
        public Container(Guid ID, string name , string description, ContainerStatus status, RoutingSlip routingSlip, DateTime createdOn )
        {
            Name = name;
            Description = description;
            Id = ID;
            Status = status;
            RoutingSlip = routingSlip;
            CreatedOn = createdOn;
        }
        public Container(Guid ID, string name , string description )
        {
            Name = name;
            Description = description;
            Id = ID;
            Status = ContainerStatus.Empty;
            RoutingSlip = null;
            CreatedOn = DateTime.UtcNow;
        }

        public ContainerInformation Information()
        {
            return new (Id, Name, Description, CreatedOn);
        }

        public string ToJsonString()
        {
            return JsonSerializer.Serialize<Container>(this);
        }
    }
}

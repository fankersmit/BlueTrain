using System;
using BlueTrain.Shared;

namespace BlueTrain.Containers
{
    public readonly struct ContainerInformation
    {
        public readonly Guid Id;
        public readonly string Name;
        public readonly string Description;
        public readonly DateTime CreatedOn;

        public ContainerInformation( Guid id, string name, string description, DateTime createdOn)
        {
            Id = id;
            Name = name;
            Description = description;
            CreatedOn = createdOn;
        }
    }
}

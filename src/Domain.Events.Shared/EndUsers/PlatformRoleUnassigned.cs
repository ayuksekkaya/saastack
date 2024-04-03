using Domain.Interfaces.Entities;

namespace Domain.Events.Shared.EndUsers;

public sealed class PlatformRoleUnassigned : IDomainEvent
{
    public required string Role { get; set; }

    public required string RootId { get; set; }

    public required DateTime OccurredUtc { get; set; }
}
using EventBus.Events;

namespace Building.IntegrationEvents.Events;

public record UserCreatedSuccessfullyIntegrationEvent : IntegrationEvent
{
    public Guid UserId { get; }

    public UserCreatedSuccessfullyIntegrationEvent(Guid userId) => UserId = userId;
}
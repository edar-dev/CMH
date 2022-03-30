using EventBus.Events;

namespace Building.IntegrationEvents.Events;

public record UserCreatedSuccessfullyIntegrationEvent : IntegrationEvent
{
    public int UserId { get; }

    public UserCreatedSuccessfullyIntegrationEvent(int userId) => UserId = userId;
}
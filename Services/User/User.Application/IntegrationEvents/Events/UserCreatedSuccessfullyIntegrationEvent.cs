
using EventBus.Events;

namespace User.Application.IntegrationEvents.Events;

public record UserCreatedSuccessfullyIntegrationEvent : IntegrationEvent
{
    public Guid UserId { get; }

    public UserCreatedSuccessfullyIntegrationEvent(Guid userId) => UserId = userId;
}
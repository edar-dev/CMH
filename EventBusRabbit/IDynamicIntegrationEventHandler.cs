namespace EventBusRabbit;

public interface IDynamicIntegrationEventHandler
{
    Task Handle(dynamic eventData);
}
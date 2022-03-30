using RabbitMQ.Client;

namespace EventBusRabbit;

public interface IRabbitMQPersistentConnection
    : IDisposable
{
    bool IsConnected { get; }

    bool TryConnect();

    IModel CreateModel();
}
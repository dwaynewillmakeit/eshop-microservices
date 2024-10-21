namespace BuildingBlock.Messaging.Events;

public record IntegrationEvent
{
    public Guid id = Guid.NewGuid();
    public DateTime OccuredOn => DateTime.Now;
    public string EventType => GetType().AssemblyQualifiedName;
}

namespace core.Interfaces.Repositories;

public interface IEventSourcedRepository<TAggregate, TId, TBaseEvent>
where TAggregate : IWithId<TId>
where TBaseEvent : IVisitorEvent<TAggregate, TId>
{
	Task<TAggregate?> GetAsync(TId id, CancellationToken cancellationToken);
	Task SaveEventAsync<TEvent>(TEvent @event, CancellationToken cancellationToken) where TEvent : TBaseEvent;
}

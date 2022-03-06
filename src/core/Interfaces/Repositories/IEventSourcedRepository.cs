namespace core.Interfaces.Repositories;

public interface IEventSourcedRepository<TAggregate, TId, TBaseCommand>
where TAggregate : IWithId<TId>
where TBaseCommand : IAggregateVisitor<TAggregate, TId>
{
	Task<TAggregate?> GetAsync(TId id, CancellationToken cancellationToken);
	Task SaveCommandAsync<TCommand>(TCommand command, CancellationToken cancellationToken) where TCommand : TBaseCommand;
}

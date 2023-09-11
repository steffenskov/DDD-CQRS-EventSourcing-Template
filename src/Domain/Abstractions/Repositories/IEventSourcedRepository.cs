namespace Domain.Abstractions.Repositories;

public interface IEventSourcedRepository<TAggregate, TAggregateId, TBaseCommand>
where TAggregate : IAggregate<TAggregateId>
where TAggregateId : StrongTypedGuid<TAggregateId>
where TBaseCommand : ICommand<TAggregate, TAggregateId>
{
	Task<TAggregate?> GetAsync(TAggregateId id, CancellationToken cancellationToken);
	Task PersistCommandAsync<TCommand>(TCommand command, CancellationToken cancellationToken) where TCommand : TBaseCommand;
}

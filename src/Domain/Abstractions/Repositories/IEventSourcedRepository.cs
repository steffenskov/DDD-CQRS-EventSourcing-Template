namespace Domain.Abstractions.Repositories;

public interface IEventSourcedRepository<TAggregate, TAggregateId, TBaseCommand>
where TAggregate : IAggregate<TAggregateId>
where TBaseCommand : IAggregateVisitor<TAggregate, TAggregateId>
{
	Task<TAggregate?> GetAsync(TAggregateId id, CancellationToken cancellationToken);
	Task SaveCommandAsync<TCommand>(TCommand command, CancellationToken cancellationToken) where TCommand : TBaseCommand;
}

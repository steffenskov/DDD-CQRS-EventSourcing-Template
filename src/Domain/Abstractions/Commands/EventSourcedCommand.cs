
namespace Domain;

public abstract record EventSourcedCommand<TAggregate, TAggregateId>(TAggregateId AggregateId) : ICommand<TAggregate, TAggregateId>
where TAggregate : IAggregate<TAggregateId>
where TAggregateId : StrongTypedGuid<TAggregateId>
{
	public CommandId Id { get; private init; } = CommandId.New();
	public DateTimeOffset Created { get; private init; } = DateTimeOffset.UtcNow;

	public abstract Task VisitAsync(TAggregate aggregate, CancellationToken cancellationToken);
}

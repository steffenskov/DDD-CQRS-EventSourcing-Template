namespace core.Interfaces.Commands;

public interface IAggregateVisitor<TAggregate, TId> // We're using the visitor pattern for our commands, to let the commands visit their Aggregate to apply changes to the aggregate
where TAggregate : IWithId<TId>
{
	TId AggregateId { get; }
	void Visit(TAggregate aggregate);
}
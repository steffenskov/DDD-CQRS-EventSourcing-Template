namespace core.Interfaces.Events;

public interface IVisitorEvent<TAggregate, TId> // We're using the visitor pattern for our events, to let the events visit their Aggregate to apply changes to the aggregate
where TAggregate : IWithId<TId>
{
	TId AggregateId { get; }
	void Visit(TAggregate aggregate);
}
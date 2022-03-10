namespace Domain.Abstractions.Commands;

public interface IAggregateVisitor<TAggregate, TAggregateId> // We're using the visitor pattern for our commands, to let the commands visit their Aggregate to apply changes to the aggregate
where TAggregate : IAggregate<TAggregateId>
{
	TAggregateId AggregateId { get; }
	void Visit(TAggregate aggregate);
}
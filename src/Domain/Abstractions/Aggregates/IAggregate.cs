namespace Domain.Abstractions.Aggregates;

public interface IAggregate<TAggregateId>
{
	TAggregateId Id { get; }
}
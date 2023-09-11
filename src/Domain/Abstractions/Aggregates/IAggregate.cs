namespace Domain.Abstractions.Aggregates;

public interface IAggregate<TAggregateId>
where TAggregateId : StrongTypedGuid<TAggregateId>
{
	TAggregateId Id { get; }
}
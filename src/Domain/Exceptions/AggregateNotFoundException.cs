namespace Domain.Exceptions;

public class AggregateNotFoundException<TAggregate, TAggregateId> : Exception
where TAggregate : IAggregate<TAggregateId>
where TAggregateId : StrongTypedGuid<TAggregateId>
{
	public AggregateNotFoundException(TAggregateId id) : base($"{typeof(TAggregate).Name} not found with id: {id}")
	{
	}
}
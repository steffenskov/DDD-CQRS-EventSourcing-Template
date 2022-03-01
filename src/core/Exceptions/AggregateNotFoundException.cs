using System.Diagnostics.Tracing;
using core.Todos.Commands;

namespace core.Exceptions;

public class AggregateNotFoundException<TAggregate, TId> : Exception
where TAggregate : IWithId<TId>
{
	public AggregateNotFoundException(TId id) : base($"{typeof(TAggregate).Name} not found with id: {id}")
	{
	}
}
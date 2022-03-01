using core.Todos.Aggregates;

namespace core.Todos.Events;

public class TodoDeleteEvent : ITodoEvent
{
	public Guid AggregateId { get; }

	public TodoDeleteEvent(Guid aggregateId)
	{
		this.AggregateId = aggregateId;
	}

	public void Visit(Todo aggregate)
	{
		aggregate.When(this);
	}
}
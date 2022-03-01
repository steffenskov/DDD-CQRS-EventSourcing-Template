using core.Todos.Aggregates;

namespace core.Todos.Events;

public class TodoUpdateDueDateEvent : ITodoEvent
{
	public Guid AggregateId { get; }
	public DateTime DueDate { get; }

	public TodoUpdateDueDateEvent(Guid aggregateId, DateTime dueDate)
	{
		this.AggregateId = aggregateId;
		this.DueDate = dueDate;
	}

	public void Visit(Todo aggregate)
	{
		aggregate.When(this);
	}
}
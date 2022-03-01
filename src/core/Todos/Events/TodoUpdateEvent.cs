using core.Todos.Aggregates;

namespace core.Todos.Events;

public class TodoUpdateEvent : ITodoEvent
{
	public Guid AggregateId { get; }

	public string Title { get; }

	public string Body { get; }

	public DateTime DueDate { get; }

	public TodoUpdateEvent(Guid aggregateId, string title, string body, DateTime dueDate)
	{
		this.AggregateId = aggregateId;
		this.Title = title;
		this.Body = body;
		this.DueDate = dueDate;
	}

	public void Visit(Todo aggregate)
	{
		aggregate.When(this);
	}
}
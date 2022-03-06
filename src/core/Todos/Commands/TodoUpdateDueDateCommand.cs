using core.Todos.Aggregates;

namespace core.Todos.Commands;

public class TodoUpdateDueDateCommand : ITodoCommand
{
	public Guid AggregateId { get; }
	public DateTime DueDate { get; }

	public TodoUpdateDueDateCommand(Guid aggregateId, DateTime dueDate)
	{
		this.AggregateId = aggregateId;
		this.DueDate = dueDate;
	}

	public void Visit(Todo aggregate)
	{
		aggregate.When(this);
	}
}
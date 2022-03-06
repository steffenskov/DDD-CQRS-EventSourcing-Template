using core.Todos.Aggregates;

namespace core.Todos.Commands;

public class TodoDeleteCommand : ITodoCommand
{
	public Guid AggregateId { get; }

	public TodoDeleteCommand(Guid aggregateId)
	{
		this.AggregateId = aggregateId;
	}

	public void Visit(Todo aggregate)
	{
		aggregate.When(this);
	}
}

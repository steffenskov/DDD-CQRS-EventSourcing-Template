using Domain.Todos.Aggregates;

namespace Domain.Todos.Commands;

public record TodoUpdateDueDateCommand(Guid AggregateId, DateTime DueDate) : ITodoCommand
{
	public void Visit(Todo aggregate)
	{
		aggregate.When(this);
	}
}
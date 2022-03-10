using Domain.Todos.Aggregates;

namespace Domain.Todos.Commands;

public record TodoCreateCommand(Guid AggregateId, string Title, string Body, DateTime DueDate) : ITodoCommand
{
	public void Visit(Todo aggregate)
	{
		aggregate.When(this);
	}
}

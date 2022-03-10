using Domain.Todos.Aggregates;

namespace Domain.Todos.Commands;

public record TodoDeleteCommand(Guid AggregateId) : ITodoCommand
{
	public void Visit(Todo aggregate)
	{
		aggregate.When(this);
	}
}

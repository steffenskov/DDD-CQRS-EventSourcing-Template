using Domain.Todos.Aggregates;

namespace Domain.Todos.Commands;

public record TodoUpdateCommand(Guid AggregateId, string Title, string Body, DateTime DueDate) : BaseTodoCommand
{
	public void Visit(Todo aggregate)
	{
		aggregate.WhenAsync(this);
	}
}
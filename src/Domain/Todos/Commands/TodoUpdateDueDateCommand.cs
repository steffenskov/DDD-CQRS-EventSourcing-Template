using Domain.Todos.Aggregates;

namespace Domain.Todos.Commands;

public record TodoUpdateDueDateCommand(Guid AggregateId, DateTime DueDate) : BaseTodoCommand
{
	public void Visit(Todo aggregate)
	{
		aggregate.WhenAsync(this);
	}
}
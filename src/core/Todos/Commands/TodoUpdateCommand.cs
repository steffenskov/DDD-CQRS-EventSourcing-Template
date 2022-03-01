using core.Todos.Aggregates;
using core.Todos.Events;

namespace core.Todos.Commands;

public class TodoUpdateCommand : ICommand<TodoUpdateEvent, Todo, Guid>
{
	public TodoUpdateEvent Event { get; }

	public TodoUpdateCommand(TodoUpdateEvent @event)
	{
		this.Event = @event;
	}
}

using core.Todos.Aggregates;
using core.Todos.Events;

namespace core.Todos.Commands;

public class TodoCreateCommand : ICommand<TodoCreateEvent, Todo, Guid>
{
	public TodoCreateEvent Event { get; }

	public TodoCreateCommand(TodoCreateEvent @event)
	{
		this.Event = @event;
	}
}

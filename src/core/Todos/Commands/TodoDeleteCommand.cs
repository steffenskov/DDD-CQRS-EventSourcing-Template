using core.Todos.Aggregates;
using core.Todos.Events;

namespace core.Todos.Commands;

public class TodoDeleteCommand : ICommand<TodoDeleteEvent, Todo, Guid>
{
	public TodoDeleteEvent Event { get; }

	public TodoDeleteCommand(TodoDeleteEvent @event)
	{
		this.Event = @event;
	}
}

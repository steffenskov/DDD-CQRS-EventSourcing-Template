using core.Todos.Aggregates;
using core.Todos.Events;

namespace core.Todos.Commands;

public class TodoUpdateDueDateCommand : ICommand<TodoUpdateDueDateEvent, Todo, Guid>
{
	public TodoUpdateDueDateEvent Event { get; }

	public TodoUpdateDueDateCommand(TodoUpdateDueDateEvent @event)
	{
		this.Event = @event;
	}
}

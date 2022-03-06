using core.Todos.Aggregates;
using core.Todos.Commands;

namespace core.Todos.Notifications;

public class TodoPersistNotification : INotification
{
	public Todo Aggregate { get; }
	public ITodoCommand Command { get; }

	public TodoPersistNotification(Todo aggregate, ITodoCommand command)
	{
		this.Aggregate = aggregate;
		this.Command = command;
	}
}
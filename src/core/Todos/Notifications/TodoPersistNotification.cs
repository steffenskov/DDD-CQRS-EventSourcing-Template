using core.Todos.Aggregates;
using core.Todos.Events;

namespace core.Todos.Notifications;

public class TodoPersistNotification : INotification
{
	public Todo Aggregate { get; }
	public ITodoEvent Event { get; }

	public TodoPersistNotification(Todo aggregate, ITodoEvent @event)
	{
		this.Aggregate = aggregate;
		this.Event = @event;
	}
}
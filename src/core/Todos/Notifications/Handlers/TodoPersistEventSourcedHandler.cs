using core.Todos.Repositories;

namespace core.Todos.Notifications.Handlers;

public class TodoPersistEventSourcedHandler
	: INotificationHandler<TodoPersistNotification>
{
	private readonly ITodoEventSourcedRepository _repository;

	public TodoPersistEventSourcedHandler(ITodoEventSourcedRepository repository)
	{
		_repository = repository;
	}

	public async Task Handle(TodoPersistNotification notification, CancellationToken cancellationToken)
	{
		await _repository.SaveEventAsync(notification.Event, cancellationToken).ConfigureAwait(false);
	}
}
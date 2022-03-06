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
		await _repository.SaveCommandAsync(notification.Command, cancellationToken).ConfigureAwait(false);
	}
}
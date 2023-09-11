using Domain.Todos.Repositories;

namespace Domain.Todos.Notifications.Handlers;

internal class TodoPersistEventSourcedHandler
	: INotificationHandler<TodoPersistNotification>
{
	private readonly ITodoEventSourcedRepository _repository;

	public TodoPersistEventSourcedHandler(ITodoEventSourcedRepository repository)
	{
		_repository = repository;
	}

	public async Task Handle(TodoPersistNotification notification, CancellationToken cancellationToken)
	{
		await _repository.PersistCommandAsync(notification.Command, cancellationToken).ConfigureAwait(false);
	}
}
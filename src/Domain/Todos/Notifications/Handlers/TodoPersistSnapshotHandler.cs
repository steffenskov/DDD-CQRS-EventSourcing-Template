using Domain.Todos.Repositories;

namespace Domain.Todos.Notifications.Handlers;

internal class TodoPersistSnapshotHandler
	: INotificationHandler<TodoPersistNotification>
{
	private readonly ITodoSnapshotRepository _repository;

	public TodoPersistSnapshotHandler(ITodoSnapshotRepository repository)
	{
		_repository = repository;
	}

	public async Task Handle(TodoPersistNotification notification, CancellationToken cancellationToken)
	{
		await _repository.PersistAggregateAsync(notification.Aggregate, cancellationToken).ConfigureAwait(false);
	}
}
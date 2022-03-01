using core.Todos.Repositories;

namespace core.Todos.Notifications.Handlers;

public class TodoPersistSnapshotHandler
	: INotificationHandler<TodoPersistNotification>
{
	private readonly ITodoSnapshotRepository _repository;

	public TodoPersistSnapshotHandler(ITodoSnapshotRepository repository)
	{
		_repository = repository;
	}

	public async Task Handle(TodoPersistNotification notification, CancellationToken cancellationToken)
	{
		await _repository.SaveAggregateAsync(notification.Aggregate, cancellationToken).ConfigureAwait(false);
	}
}
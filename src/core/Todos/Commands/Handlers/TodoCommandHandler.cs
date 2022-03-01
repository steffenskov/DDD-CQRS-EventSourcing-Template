using core.Todos.Aggregates;
using core.Todos.Events;
using core.Todos.Notifications;
using core.Todos.Repositories;

namespace core.Todos.Commands.Handlers;

public class TodoCommandHandler
	: IRequestHandler<TodoCreateCommand, Todo>,
	IRequestHandler<TodoUpdateCommand, Todo>,
	IRequestHandler<TodoUpdateDueDateCommand, Todo>,
	IRequestHandler<TodoDeleteCommand, Todo>
{
	private readonly ITodoSnapshotRepository _snapshotRepository;
	private readonly IMediator _mediator;

	public TodoCommandHandler(ITodoSnapshotRepository snapshotRepository, IMediator mediator)
	{
		_snapshotRepository = snapshotRepository;
		_mediator = mediator;
	}

	public async Task<Todo> Handle(TodoCreateCommand request, CancellationToken cancellationToken)
	{
		var todo = new Todo();
		todo.When(request.Event);
		await SendPersistanceNotification(todo, request.Event).ConfigureAwait(false);

		return todo;
	}

	public async Task<Todo> Handle(TodoUpdateCommand request, CancellationToken cancellationToken)
	{
		var todo = await GetTodoAsync(request.Event.AggregateId, cancellationToken).ConfigureAwait(false);
		todo.When(request.Event);
		await SendPersistanceNotification(todo, request.Event).ConfigureAwait(false);

		return todo;
	}

	public async Task<Todo> Handle(TodoUpdateDueDateCommand request, CancellationToken cancellationToken)
	{
		var todo = await GetTodoAsync(request.Event.AggregateId, cancellationToken).ConfigureAwait(false);
		todo.When(request.Event);
		await SendPersistanceNotification(todo, request.Event).ConfigureAwait(false);

		return todo;
	}

	public async Task<Todo> Handle(TodoDeleteCommand request, CancellationToken cancellationToken)
	{
		var todo = await GetTodoAsync(request.Event.AggregateId, cancellationToken).ConfigureAwait(false);
		todo.When(request.Event);

		await SendPersistanceNotification(todo, request.Event).ConfigureAwait(false);
		return todo;
	}

	private async Task<Todo> GetTodoAsync(Guid id, CancellationToken cancellationToken)
	{
		return await _snapshotRepository.GetAsync(id, cancellationToken).ConfigureAwait(false) ?? throw new AggregateNotFoundException<Todo, Guid>(id);
	}

	private async Task SendPersistanceNotification<TEvent>(Todo aggregate, TEvent @event)
	where TEvent : ITodoEvent
	{
		var persistanceNotification = new TodoPersistNotification(aggregate, @event);
		await _mediator.Publish(persistanceNotification).ConfigureAwait(false); // Note how we don't include the cancellation token, this is because we don't want part of the persist to go through, and part not to. Cancellation midway through persistance is simply not a good idea.
	}
}

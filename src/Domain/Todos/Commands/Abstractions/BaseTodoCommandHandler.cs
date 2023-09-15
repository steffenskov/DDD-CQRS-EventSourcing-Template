namespace Domain.Todos.Commands.Abstractions;

internal abstract class BaseTodoCommandHandler<T> : IRequestHandler<T, Todo>
where T : BaseTodoCommand
{
	protected readonly IMediator _mediator;
	private readonly ITodoSnapshotRepository _repository;
	private readonly ITodoEventSourcedRepository _eventSourcedRepository;

	protected BaseTodoCommandHandler(IMediator mediator, ITodoSnapshotRepository repository, ITodoEventSourcedRepository eventSourcedRepository)
	{
		_mediator = mediator;
		_repository = repository;
		_eventSourcedRepository = eventSourcedRepository;
	}

	protected abstract Task<Todo> GetAggregateAsync(T request, CancellationToken cancellationToken);

	public async Task<Todo> Handle(T request, CancellationToken cancellationToken)
	{
		var aggregate = await GetAggregateAsync(request, cancellationToken) ?? throw new AggregateNotFoundException<Todo, TodoId>(request.AggregateId);
		var aggregateToPersist = await request.VisitAsync(aggregate, cancellationToken);
		await _eventSourcedRepository.PersistCommandAsync(request, cancellationToken);
		await _repository.PersistAggregateAsync(aggregateToPersist, cancellationToken); // Consider adding resilience here, using e.g. Polly to ensure both repositories gets persisted
		return aggregateToPersist;
	}
}

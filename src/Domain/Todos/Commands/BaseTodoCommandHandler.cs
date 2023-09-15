namespace Domain.Todos.Commands;

internal abstract class BaseTodoCommandHandler<T> : IRequestHandler<T, Todo>
where T : BaseTodoCommand
{
	private readonly ITodoSnapshotRepository _repository;
	private readonly ITodoEventSourcedRepository _eventSourcedRepository;

	protected BaseTodoCommandHandler(ITodoSnapshotRepository repository, ITodoEventSourcedRepository eventSourcedRepository)
	{
		_repository = repository;
		_eventSourcedRepository = eventSourcedRepository;
	}

	public async Task<Todo> Handle(T request, CancellationToken cancellationToken)
	{
		var aggregate = await _repository.GetAsync(request.AggregateId, cancellationToken) ?? throw new AggregateNotFoundException<Todo, TodoId>(request.AggregateId);
		var aggregateToPersist = await request.VisitAsync(aggregate, cancellationToken);
		await _eventSourcedRepository.PersistCommandAsync(request, cancellationToken);
		await _repository.PersistAggregateAsync(aggregateToPersist, cancellationToken); // Consider adding resilience here, using e.g. Polly to ensure both repositories gets persisted
		return aggregateToPersist;
	}
}

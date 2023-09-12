namespace Domain.Todos.Commands;

internal abstract class BaseTodoCommandHandler<T> : IRequestHandler<T, Todo>
where T : BaseTodoCommand
{
	private readonly ITodoSnapshotRepository _repository;

	protected BaseTodoCommandHandler(ITodoSnapshotRepository repository)
	{
		this._repository = repository;
	}

	public async Task<Todo> Handle(T request, CancellationToken cancellationToken)
	{
		var aggregate = await _repository.GetAsync(request.AggregateId, cancellationToken) ?? throw new AggregateNotFoundException<Todo, TodoId>(request.AggregateId);
		var aggregateToPersist = await request.VisitAsync(aggregate, cancellationToken);
		await _repository.PersistAggregateAsync(aggregateToPersist, cancellationToken);
		return aggregateToPersist;
	}
}

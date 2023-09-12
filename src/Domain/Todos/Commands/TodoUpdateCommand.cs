namespace Domain.Todos.Commands;

public sealed record TodoUpdateCommand(TodoId AggregateId, string Title, string Body, DateTime DueDate) : BaseTodoCommand(AggregateId)
{
	public override async Task<Todo> VisitAsync(Todo aggregate, CancellationToken cancellationToken)
	{
		return await aggregate.WithAsync(this, cancellationToken);
	}
}

sealed file class Handler : IRequestHandler<TodoUpdateCommand, Todo>
{
	private readonly ITodoSnapshotRepository _repository;

	public Handler(ITodoSnapshotRepository repository)
	{
		_repository = repository;
	}

	public async Task<Todo> Handle(TodoUpdateCommand request, CancellationToken cancellationToken)
	{
		var aggregate = await _repository.GetAsync(request.AggregateId, cancellationToken) ?? throw new AggregateNotFoundException<Todo, TodoId>(request.AggregateId);
		var aggregateToPersist = await request.VisitAsync(aggregate, cancellationToken);
		await _repository.PersistAggregateAsync(aggregateToPersist, cancellationToken);
		return aggregateToPersist;
	}
}
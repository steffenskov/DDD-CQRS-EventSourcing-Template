namespace Domain.Todos.Commands;

public sealed record TodoCreateCommand(TodoId AggregateId, string Title, string Body, DateTime DueDate) : BaseTodoCommand(AggregateId)
{
	public override async Task<Todo> VisitAsync(Todo aggregate, CancellationToken cancellationToken)
	{
		return await aggregate.WithAsync(this, cancellationToken);
	}
}

sealed file class Handler : IRequestHandler<TodoCreateCommand, Todo>
{
	private readonly ITodoSnapshotRepository _repository;

	public Handler(ITodoSnapshotRepository repository)
	{
		_repository = repository;
	}

	public async Task<Todo> Handle(TodoCreateCommand request, CancellationToken cancellationToken)
	{
		var aggregate = new Todo();
		var aggregateToPersist = await aggregate.WithAsync(request, cancellationToken);
		await _repository.PersistAggregateAsync(aggregateToPersist, cancellationToken);
		return aggregateToPersist;
	}
}
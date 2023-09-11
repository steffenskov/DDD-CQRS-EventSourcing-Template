namespace Domain.Todos.Commands;

public record TodoDeleteCommand(TodoId AggregateId) : BaseTodoCommand(AggregateId)
{
	public override async Task VisitAsync(Todo aggregate, CancellationToken cancellationToken)
	{
		await aggregate.WhenAsync(this, cancellationToken);
	}
}

sealed file class Handler : IRequestHandler<TodoCreateCommand, Todo?>
{
	private readonly ITodoSnapshotRepository _repository;

	public Handler(ITodoSnapshotRepository repository)
	{
		_repository = repository;

	}

	public async Task<Todo?> Handle(TodoCreateCommand request, CancellationToken cancellationToken)
	{
		var aggregate = await _repository.GetAsync(request.AggregateId, cancellationToken);
		if (aggregate is null)
		{
			return null;
		}
		await aggregate.WhenAsync(request, cancellationToken);
		await _repository.PersistAggregateAsync(aggregate, cancellationToken);
		return aggregate;
	}
}
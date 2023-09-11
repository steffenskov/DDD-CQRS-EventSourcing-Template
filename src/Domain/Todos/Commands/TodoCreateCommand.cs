namespace Domain.Todos.Commands;

public record TodoCreateCommand(TodoId AggregateId, string Title, string Body, DateTime DueDate) : BaseTodoCommand(AggregateId)
{
	public override async Task VisitAsync(Todo aggregate, CancellationToken cancellationToken)
	{
		await aggregate.WhenAsync(this, cancellationToken);
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
		await aggregate.WhenAsync(request, cancellationToken);
		await _repository.PersistAggregateAsync(aggregate, cancellationToken);
		return aggregate;
	}
}
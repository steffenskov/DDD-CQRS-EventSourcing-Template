namespace Domain.Todos.Queries;

public sealed record TodoGetSingleQuery(TodoId AggregateId) : IQuery<Todo?>;

file sealed class Handler : BaseTodoQueryHandler<TodoGetSingleQuery, Todo?>
{
	public Handler(ITodoSnapshotRepository repository) : base(repository)
	{
	}

	public override async Task<Todo?> Handle(TodoGetSingleQuery request, CancellationToken cancellationToken)
	{
		return await _repository.GetAsync(request.AggregateId, cancellationToken);
	}
}
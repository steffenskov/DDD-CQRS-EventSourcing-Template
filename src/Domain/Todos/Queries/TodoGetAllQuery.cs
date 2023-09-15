namespace Domain.Todos.Queries;

public sealed record TodoGetAllQuery() : IQuery<IEnumerable<Todo>>;

file sealed class Handler : BaseTodoQueryHandler<TodoGetAllQuery, IEnumerable<Todo>>
{
	public Handler(ITodoSnapshotRepository repository) : base(repository)
	{
	}

	public override async Task<IEnumerable<Todo>> Handle(TodoGetAllQuery request, CancellationToken cancellationToken)
	{
		return await _repository.GetAllAsync(cancellationToken);
	}
}

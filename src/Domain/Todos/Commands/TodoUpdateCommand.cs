

namespace Domain.Todos.Commands;

public sealed record TodoUpdateCommand(TodoId AggregateId, string Title, string Body, DateTime DueDate) : BaseTodoCommand(AggregateId)
{
	public override async Task<Todo> VisitAsync(Todo aggregate, CancellationToken cancellationToken)
	{
		return await aggregate.WithAsync(this, cancellationToken);
	}
}

sealed file class Handler : BaseTodoCommandHandler<TodoUpdateCommand>
{
	public Handler(ITodoSnapshotRepository repository, ITodoEventSourcedRepository eventSourcedRepository) : base(repository, eventSourcedRepository)
	{
	}
}
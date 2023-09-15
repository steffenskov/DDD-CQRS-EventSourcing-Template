
namespace Domain.Todos.Commands;

public sealed record TodoUpdateDueDateCommand(TodoId AggregateId, DateTime DueDate) : BaseTodoCommand(AggregateId)
{
	public override async Task<Todo> VisitAsync(Todo aggregate, CancellationToken cancellationToken)
	{
		return await aggregate.WithAsync(this, cancellationToken);
	}
}


sealed file class Handler : BaseTodoCommandHandler<TodoUpdateDueDateCommand>
{
	public Handler(ITodoSnapshotRepository repository, ITodoEventSourcedRepository eventSourcedRepository) : base(repository, eventSourcedRepository)
	{
	}
}
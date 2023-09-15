namespace Domain.Todos.Commands;

public sealed record TodoUpdateDueDateCommand(TodoId AggregateId, DateTime DueDate) : BaseTodoCommand(AggregateId)
{
	public override async Task<Todo> VisitAsync(Todo aggregate, CancellationToken cancellationToken)
	{
		return await aggregate.WithAsync(this, cancellationToken);
	}
}


sealed file class Handler : BaseTodoUpdateCommandHandler<TodoUpdateDueDateCommand>
{
	public Handler(IMediator mediator, ITodoSnapshotRepository repository, ITodoEventSourcedRepository eventSourcedRepository) : base(mediator, repository, eventSourcedRepository)
	{
	}
}
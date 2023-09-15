namespace Domain.Todos.Commands;

public sealed record TodoUpdateCommand(TodoId AggregateId, string Title, string Body, DateTime DueDate) : BaseTodoCommand(AggregateId)
{
	public override async Task<Todo> VisitAsync(Todo aggregate, CancellationToken cancellationToken)
	{
		return await aggregate.WithAsync(this, cancellationToken);
	}
}

sealed file class Handler : BaseTodoUpdateCommandHandler<TodoUpdateCommand>
{
	public Handler(IMediator mediator, ITodoSnapshotRepository repository, ITodoEventSourcedRepository eventSourcedRepository) : base(mediator, repository, eventSourcedRepository)
	{
	}
}
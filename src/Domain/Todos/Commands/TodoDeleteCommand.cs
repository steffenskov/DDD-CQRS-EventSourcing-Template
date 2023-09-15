namespace Domain.Todos.Commands;

public sealed record TodoDeleteCommand(TodoId AggregateId) : BaseTodoCommand(AggregateId)
{
	public override async Task<Todo> VisitAsync(Todo aggregate, CancellationToken cancellationToken)
	{
		return await aggregate.WithAsync(this, cancellationToken);
	}
}

sealed file class Handler : BaseTodoUpdateCommandHandler<TodoCreateCommand>
{
	public Handler(IMediator mediator, ITodoSnapshotRepository repository, ITodoEventSourcedRepository eventSourcedRepository) : base(mediator, repository, eventSourcedRepository)
	{
	}
}
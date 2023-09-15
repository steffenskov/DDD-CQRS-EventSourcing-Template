namespace Domain.Todos.Commands;

public sealed record TodoCreateCommand(TodoId AggregateId, string Title, string Body, DateTime DueDate) : BaseTodoCommand(AggregateId)
{
	public override async Task<Todo> VisitAsync(Todo aggregate, CancellationToken cancellationToken)
	{
		return await aggregate.WithAsync(this, cancellationToken);
	}
}

sealed file class Handler : BaseTodoCommandHandler<TodoCreateCommand>
{
	public Handler(IMediator mediator, ITodoSnapshotRepository repository, ITodoEventSourcedRepository eventSourcedRepository) : base(mediator, repository, eventSourcedRepository)
	{
	}

	protected override Task<Todo> GetAggregateAsync(TodoCreateCommand request, CancellationToken cancellationToken)
	{
		return Task.FromResult(new Todo());
	}
}
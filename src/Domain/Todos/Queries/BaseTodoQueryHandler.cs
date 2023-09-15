namespace Domain;

internal abstract class BaseTodoQueryHandler<TQuery, TResult> : IRequestHandler<TQuery, TResult>
where TQuery : IRequest<TResult>
{
	protected readonly ITodoSnapshotRepository _repository;

	protected BaseTodoQueryHandler(ITodoSnapshotRepository repository)
	{
		this._repository = repository;
	}

	public abstract Task<TResult> Handle(TQuery request, CancellationToken cancellationToken);
}

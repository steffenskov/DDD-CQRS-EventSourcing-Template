using core.Todos.Aggregates;
using core.Todos.Repositories;

namespace core.Todos.Queries.Handlers;

public class TodoQueryHandler
	: IRequestHandler<TodoGetSingleQuery, Todo?>,
	IRequestHandler<TodoGetAllQuery, IEnumerable<Todo>>

{
	// If you don't want to use snapshots to speed up data retrieval, you can replace this with an IEventSourcedRepository
	private readonly ITodoSnapshotRepository _snapshotRepository;

	public TodoQueryHandler(ITodoSnapshotRepository snapshotRepository)
	{
		_snapshotRepository = snapshotRepository;
	}

	public async Task<Todo?> Handle(TodoGetSingleQuery request, CancellationToken cancellationToken)
	{
		return await _snapshotRepository.GetAsync(request.Id, cancellationToken).ConfigureAwait(false);
	}

	public async Task<IEnumerable<Todo>> Handle(TodoGetAllQuery request, CancellationToken cancellationToken)
	{
		return await _snapshotRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
	}
}

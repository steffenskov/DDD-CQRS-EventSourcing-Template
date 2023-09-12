using System.Collections.Concurrent;

namespace Infrastructure.Todos.Repositories;

// This is an extremely crude implementation, obviously you'd have something better here.
// Do note how the class is internal, as no other projects should ever know if even exists as we're using dependency injection to ensure the interface has an implementation.
internal class TodoSnapshotRepository : ITodoSnapshotRepository
{
	private static IDictionary<Guid, Todo> _data = new ConcurrentDictionary<Guid, Todo>(); // We use a static dictionary to mimic an actual data store, like e.g. a SQL server

	public Task<IEnumerable<Todo>> GetAllAsync(CancellationToken cancellationToken)
	{
		return Task.FromResult((IEnumerable<Todo>)_data.Values);
	}

	public Task<Todo?> GetAsync(Guid id, CancellationToken cancellationToken)
	{
		if (_data.TryGetValue(id, out var result))
			return Task.FromResult((Todo?)result);

		return Task.FromResult((Todo?)null);
	}

	public Task PersistAggregateAsync(Todo aggregate, CancellationToken cancellationToken)
	{
		if (aggregate.Deleted)
			_data.Remove(aggregate.Id);
		else
			_data[aggregate.Id] = aggregate;
		return Task.CompletedTask;
	}
}
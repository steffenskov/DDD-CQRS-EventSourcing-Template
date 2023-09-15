using Domain.Todos.Commands.Abstractions;

namespace Infrastructure.Todos.Repositories;

// This is an extremely crude implementation, obviously you'd have something better here.
// Do note how the class is internal, as no other projects should ever know if even exists as we're using dependency injection to ensure the interface has an implementation.
internal sealed class TodoEventSourcedRepository : ITodoEventSourcedRepository
{
	private static readonly IList<BaseTodoCommand> _commandStore = new List<BaseTodoCommand>(); // We use a static list to mimic an actual data store, like e.g. a Cosmos DB
	private static readonly SemaphoreSlim _lock = new(1, 1);

	// This method isn't used any where in the template project, however it's included to show how you'd go about read full aggregates from an event sourced repository
	public async Task<Todo?> GetAsync(TodoId id, CancellationToken cancellationToken)
	{
		await _lock.WaitAsync(cancellationToken);
		try
		{
			var commands = _commandStore
										.Where(c => c.AggregateId == id)
										.ToList();
			if (commands.Any())
			{
				return await Todo.HydrateAsync(commands, cancellationToken);
			}
		}
		finally
		{
			_lock.Release();
		}
		return null;
	}

	public async Task PersistCommandAsync<TCommand>(TCommand command, CancellationToken cancellationToken) where TCommand : BaseTodoCommand
	{
		await _lock.WaitAsync(cancellationToken);
		try
		{
			_commandStore.Add(command);
		}
		finally
		{
			_lock.Release();
		}
	}
}
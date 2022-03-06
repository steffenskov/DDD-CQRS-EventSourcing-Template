using System.Diagnostics.Tracing;
using core.Todos.Aggregates;
using core.Todos.Commands;

namespace infrastructure.Todos.Repositories;

// This is an extremely crude implementation, obviously you'd have something better here.
// Do note how the class is internal, as no other projects should ever know if even exists as we're using dependency injection to ensure the interface has an implementation.
internal class TodoEventSourcedRepository : ITodoEventSourcedRepository
{
	private static IList<ITodoCommand> _commands = new List<ITodoCommand>();
	private static object _lock = new();

	// This method isn't used any where in the template project, however it's included to show how you'd go about read full aggregates from an event sourced repository
	public Task<Todo?> GetAsync(Guid id, CancellationToken cancellationToken)
	{
		lock (_lock)
		{
			var aggregateCommands = _commands
									.Where(command => command.AggregateId == id)
									.ToList();
			if (aggregateCommands.Any())
			{
				var todo = new Todo(aggregateCommands);
				return Task.FromResult((Todo?)todo);
			}
		}
		return Task.FromResult((Todo?)null);
	}

	public Task SaveCommandAsync<TCommand>(TCommand command, CancellationToken cancellationToken) where TCommand : ITodoCommand
	{
		lock (_lock)
		{
			_commands.Add(command);
		}
		return Task.CompletedTask;
	}
}
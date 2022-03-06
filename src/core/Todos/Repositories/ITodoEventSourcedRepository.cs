using core.Todos.Aggregates;
using core.Todos.Commands;

namespace core.Todos.Repositories;

public interface ITodoEventSourcedRepository : IEventSourcedRepository<Todo, Guid, ITodoCommand>
{
}

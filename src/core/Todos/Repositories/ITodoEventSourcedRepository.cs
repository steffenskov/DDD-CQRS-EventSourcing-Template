using core.Todos.Aggregates;
using core.Todos.Events;

namespace core.Todos.Repositories;

public interface ITodoEventSourcedRepository : IEventSourcedRepository<Todo, Guid, ITodoEvent>
{
}

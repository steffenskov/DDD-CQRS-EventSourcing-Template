using core.Todos.Aggregates;

namespace core.Todos.Events;

public interface ITodoEvent : IVisitorEvent<Todo, Guid>
{

}
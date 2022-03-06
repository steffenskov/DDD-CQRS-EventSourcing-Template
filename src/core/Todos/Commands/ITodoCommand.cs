using core.Todos.Aggregates;

namespace core.Todos.Commands;

public interface ITodoCommand : ICommand<Todo, Guid>
{

}
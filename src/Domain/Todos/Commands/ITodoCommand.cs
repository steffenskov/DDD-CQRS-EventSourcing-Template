using Domain.Todos.Aggregates;

namespace Domain.Todos.Commands;

public interface ITodoCommand : ICommand<Todo, Guid>
{

}
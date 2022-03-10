using Domain.Todos.Aggregates;
using Domain.Todos.Commands;

namespace Domain.Todos.Repositories;

/// <Summary>
/// Even though this interface is available outside the domain project, it's not intended to be used directly.
/// Rather you should send commands and queries via MediatR.
/// The only reason this interface is public, is for DependencyInjection in the infrastructure project (This is how the onion architecture works)
/// </Summary>
public interface ITodoEventSourcedRepository : IEventSourcedRepository<Todo, Guid, ITodoCommand>
{
}

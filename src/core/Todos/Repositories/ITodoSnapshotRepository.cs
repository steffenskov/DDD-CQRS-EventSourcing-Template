using core.Todos.Aggregates;

namespace core.Todos.Repositories;

public interface ITodoSnapshotRepository : ISnapshotRepository<Todo, Guid>
{
}

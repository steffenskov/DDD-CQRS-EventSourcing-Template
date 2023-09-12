

namespace Domain.Todos.Queries;

public record TodoGetAllQuery() : IQuery<IEnumerable<Todo>>;
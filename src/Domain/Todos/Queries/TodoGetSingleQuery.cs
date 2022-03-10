using Domain.Todos.Aggregates;

namespace Domain.Todos.Queries;

public record TodoGetSingleQuery(Guid AggregateId) : IQuery<Todo?>;
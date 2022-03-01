using core.Todos.Aggregates;

namespace core.Todos.Queries;

public class TodoGetAllQuery : IQuery<IEnumerable<Todo>>
{
	public TodoGetAllQuery()
	{
	}
}

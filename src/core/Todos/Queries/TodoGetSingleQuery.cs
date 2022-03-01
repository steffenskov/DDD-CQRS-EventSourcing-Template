using core.Todos.Aggregates;

namespace core.Todos.Queries;

public class TodoGetSingleQuery : IQuery<Todo?>
{
	public Guid Id { get; }

	public TodoGetSingleQuery(Guid id)
	{
		this.Id = id;
	}
}

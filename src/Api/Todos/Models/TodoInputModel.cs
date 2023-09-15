namespace Api.Todos.Models;

public class TodoInputModel
{
	public string Title { get; init; } = default!; // set or init are required for the framework to actually set the value, I prefer init to ensure nobody tampers with the value afterwards

	public string Body { get; init; } = default!;

	public DateTime DueDate { get; init; }
}

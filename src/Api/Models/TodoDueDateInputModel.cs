#nullable disable // Disabling nullability check for API models used as arguments as these are always instantiated by the framework
namespace Api.Models;

public class TodoDueDateInputModel
{
	public DateTime DueDate { get; init; } // set or init are required for the framework to actually set the value, I prefer init to ensure nobody tampers with the value afterwards
}

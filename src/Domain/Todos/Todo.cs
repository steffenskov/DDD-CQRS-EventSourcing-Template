using Domain.Todos.Commands;

namespace Domain.Todos;

// The aggregate class is public, but all command handling is kept internal to force the developer to go through CQRS.
public record Todo : IAggregate<TodoId>
{
	public static async Task<Todo> HydrateAsync(IEnumerable<BaseTodoCommand> commands, CancellationToken cancellationToken)
	{
		var aggregate = new Todo();
		foreach (var command in commands)
		{
			await command.VisitAsync(aggregate, cancellationToken);
		}
		return aggregate;
	}

	public TodoId Id { get; private set; } = default!;
	public string Title { get; private set; } = default!;
	public string Body { get; private set; } = default!;
	public DateTime DueDate { get; private set; }
	public bool Deleted { get; private set; }

	public Todo()
	{
	}

	internal Task WhenAsync(TodoCreateCommand command, CancellationToken cancellationToken)
	{
		// Validate ALL domain rules relevant for this event, prior to setting any properties to avoid an aggregate stuck in limbo.
		// Normally some rules require a MediatR query, and as such requires async functionality. This has been omitted in this example for brevity.
		ValidateBody(command.Body);
		ValidateTitle(command.Title);
		ValidateDueDate(command.DueDate);

		this.Deleted = false; // A new todo is obviously not deleted
		this.Id = command.AggregateId;
		this.Title = command.Title;
		this.Body = command.Body;
		this.DueDate = command.DueDate;

		return Task.CompletedTask;
	}

	internal Task WhenAsync(TodoUpdateCommand command, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(command);
		ValidateBody(command.Body);
		ValidateTitle(command.Title);
		ValidateDueDate(command.DueDate);

		this.Title = command.Title;
		this.Body = command.Body;
		this.DueDate = command.DueDate;

		return Task.CompletedTask;
	}

	internal Task WhenAsync(TodoUpdateDueDateCommand command, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(command);
		ValidateDueDate(command.DueDate);

		this.DueDate = command.DueDate;

		return Task.CompletedTask;
	}

	internal Task WhenAsync(TodoDeleteCommand command, CancellationToken cancellationToken)
	{
		// Consider validating whether the todo is in a valid state to be deleted, there could be e.g. related aggregates depending on this
		this.Deleted = true;

		return Task.CompletedTask;
	}

	// Validation rules are kept separately from setting the actual values to allow us to complete all validation for a given event, prior to setting any properties.
	// Often they can be kept static as well, but feel free to implement non-static methods where necessary.
	private static void ValidateBody(string body)
	{
		ArgumentNullException.ThrowIfNull(body);
	}

	private static void ValidateTitle(string title)
	{
		if (string.IsNullOrWhiteSpace(title))
			throw new ArgumentException("Title cannot be empty", nameof(title));
	}

	private static void ValidateDueDate(DateTime dueDate)
	{
		if (dueDate < DateTime.UtcNow)
		{
			throw new ArgumentOutOfRangeException(nameof(dueDate), "DueDate must be in the future");
		}
	}
}
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

	public TodoId Id { get; private init; } = default!;
	public string Title { get; private init; } = default!;
	public string Body { get; private init; } = default!;
	public DateTime DueDate { get; private init; }
	public bool Deleted { get; private init; }

	public Todo()
	{
	}

	internal Task<Todo> WithAsync(TodoCreateCommand command, CancellationToken cancellationToken)
	{
		// Validate ALL domain rules relevant for this event, prior to setting any properties to avoid an aggregate stuck in limbo.
		// Normally some rules require a MediatR query, and as such requires async functionality. This has been omitted in this example for brevity.
		ValidateBody(command.Body);
		ValidateTitle(command.Title);
		ValidateDueDate(command.DueDate);
		var result = this with
		{
			Deleted = false,  // A new todo is obviously not deleted
			Id = command.AggregateId,
			Title = command.Title,
			Body = command.Body,
			DueDate = command.DueDate
		};
		return Task.FromResult(result);
	}

	internal Task<Todo> WithAsync(TodoUpdateCommand command, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(command);
		ValidateBody(command.Body);
		ValidateTitle(command.Title);
		ValidateDueDate(command.DueDate);

		var result = this with
		{
			Title = command.Title,
			Body = command.Body,
			DueDate = command.DueDate
		};

		return Task.FromResult(result);
	}

	internal Task<Todo> WithAsync(TodoUpdateDueDateCommand command, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(command);
		ValidateDueDate(command.DueDate);
		var result = this with { DueDate = command.DueDate };

		return Task.FromResult(result);
	}

	internal Task<Todo> WithAsync(TodoDeleteCommand command, CancellationToken cancellationToken)
	{
		// Consider validating whether the todo is in a valid state to be deleted, there could be e.g. related aggregates depending on this
		var result = this with
		{
			Deleted = true
		};
		return Task.FromResult(result);
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
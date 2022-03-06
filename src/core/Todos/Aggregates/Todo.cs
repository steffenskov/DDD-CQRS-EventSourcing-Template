using core.Todos.Commands;

namespace core.Todos.Aggregates;

// The aggregate class is public, but the empty constructor and all command handling is kept internal to force the developer to go through CQRS.
public class Todo : IWithId<Guid>
{
	public Guid Id { get; private set; }
	public string Title { get; private set; } = default!;
	public string Body { get; private set; } = default!;
	public DateTime DueDate { get; private set; }
	public bool Deleted { get; private set; }

	internal Todo()
	{
	}

	public Todo(IEnumerable<ITodoCommand> commands)
	{
		// By replaying all commands in order on the aggregate, we can hydrate it to its present state
		foreach (var command in commands)
		{
			command.Visit(this);
		}
	}

	internal void When(TodoCreateCommand command)
	{
		// Validate ALL domain rules relevant for this event, prior to setting any properties to avoid an aggregate stuck in limbo.
		ArgumentNullException.ThrowIfNull(command);
		ValidateBody(command.Body);
		ValidateTitle(command.Title);
		ValidateDueDate(command.DueDate);

		this.Deleted = false; // A new todo is obviously not deleted
		this.Id = command.AggregateId;
		this.Title = command.Title;
		this.Body = command.Body;
		this.DueDate = command.DueDate;
	}

	internal void When(TodoUpdateCommand command)
	{
		ArgumentNullException.ThrowIfNull(command);
		ValidateBody(command.Body);
		ValidateTitle(command.Title);
		ValidateDueDate(command.DueDate);

		this.Title = command.Title;
		this.Body = command.Body;
		this.DueDate = command.DueDate;
	}

	internal void When(TodoUpdateDueDateCommand command)
	{
		ArgumentNullException.ThrowIfNull(command);
		ValidateDueDate(command.DueDate);

		this.DueDate = command.DueDate;
	}

	internal void When(TodoDeleteCommand command)
	{
		// Consider validating whether the todo is in a valid state to be deleted, there could be e.g. related aggregates depending on this
		this.Deleted = true;
	}

	// Validation rules are kept separately from setting the actual values to allow us to complete all validation for a given event, prior to setting any properties.
	private void ValidateBody(string body)
	{
		ArgumentNullException.ThrowIfNull(body);
	}

	private void ValidateTitle(string title)
	{
		if (string.IsNullOrWhiteSpace(title))
			throw new ArgumentException("Title cannot be empty", nameof(title));
	}

	private void ValidateDueDate(DateTime dueDate)
	{
		if (dueDate < DateTime.UtcNow)
		{
			throw new ArgumentOutOfRangeException(nameof(dueDate), "DueDate must be in the future");
		}
	}
}
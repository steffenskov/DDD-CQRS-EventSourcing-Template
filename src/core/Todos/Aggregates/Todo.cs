using core.Todos.Events;

namespace core.Todos.Aggregates;

// The aggregate class is public, but the empty constructor and all event handling is kept internal to force the developer to go through CQRS.
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

	public Todo(IEnumerable<ITodoEvent> events)
	{
		// By replaying all events in order on the aggregate, we can hydrate it to its present state
		foreach (var @event in events)
		{
			@event.Visit(this);
		}
	}

	internal void When(TodoCreateEvent createEvent)
	{
		// Validate ALL domain rules relevant for this event, prior to setting any properties to avoid an aggregate stuck in limbo.
		ArgumentNullException.ThrowIfNull(createEvent);
		ValidateBody(createEvent.Body);
		ValidateTitle(createEvent.Title);
		ValidateDueDate(createEvent.DueDate);

		this.Deleted = false; // A new todo is obviously not deleted
		this.Id = createEvent.AggregateId;
		this.Title = createEvent.Title;
		this.Body = createEvent.Body;
		this.DueDate = createEvent.DueDate;
	}

	internal void When(TodoUpdateEvent updateEvent)
	{
		ArgumentNullException.ThrowIfNull(updateEvent);
		ValidateBody(updateEvent.Body);
		ValidateTitle(updateEvent.Title);
		ValidateDueDate(updateEvent.DueDate);

		this.Title = updateEvent.Title;
		this.Body = updateEvent.Body;
		this.DueDate = updateEvent.DueDate;
	}

	internal void When(TodoUpdateDueDateEvent updateDueDateEvent)
	{
		ArgumentNullException.ThrowIfNull(updateDueDateEvent);
		ValidateDueDate(updateDueDateEvent.DueDate);

		this.DueDate = updateDueDateEvent.DueDate;
	}

	internal void When(TodoDeleteEvent deleteEvent)
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
namespace core.Interfaces.Commands;

/// <summary>
/// Implement this interface if your command doesn't return anything.
/// </summary>
public interface ICommand<TEvent, TAggregate, TId> : IRequest<TAggregate>
where TAggregate : IWithId<TId>
where TEvent : IVisitorEvent<TAggregate, TId>
{
	public TEvent Event { get; }
}
namespace core.Interfaces.Commands;

/// <summary>
/// Implement this interface for all your commands.
/// </summary>
public interface ICommand<TAggregate, TId> : IRequest<TAggregate>, IAggregateVisitor<TAggregate, TId>
where TAggregate : IWithId<TId>
{
}
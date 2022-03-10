namespace Domain.Abstractions.Commands;

/// <summary>
/// Implement this interface for all your commands.
/// </summary>
public interface ICommand<TAggregate, TAggregateId> : IRequest<TAggregate>, IAggregateVisitor<TAggregate, TAggregateId>
where TAggregate : IAggregate<TAggregateId>
{
}
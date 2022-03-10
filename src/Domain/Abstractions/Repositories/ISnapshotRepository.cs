namespace Domain.Abstractions.Repositories;

public interface ISnapshotRepository<TAggregate, TId>
where TAggregate : IAggregate<TId>
{
	Task<IEnumerable<TAggregate>> GetAllAsync(CancellationToken cancellationToken);
	Task<TAggregate?> GetAsync(TId id, CancellationToken cancellationToken);
	Task SaveAggregateAsync(TAggregate aggregate, CancellationToken cancellationToken);
}

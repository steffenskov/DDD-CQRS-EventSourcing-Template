namespace core.Interfaces.Repositories;

public interface ISnapshotRepository<TAggregate, TId>
where TAggregate : IWithId<TId>
{
	Task<IEnumerable<TAggregate>> GetAllAsync(CancellationToken cancellationToken);
	Task<TAggregate?> GetAsync(TId id, CancellationToken cancellationToken);
	Task SaveAggregateAsync(TAggregate aggregate, CancellationToken cancellationToken);
}

namespace core.Interfaces.Aggregates;

public interface IWithId<TId>
{
	TId Id { get; }
}
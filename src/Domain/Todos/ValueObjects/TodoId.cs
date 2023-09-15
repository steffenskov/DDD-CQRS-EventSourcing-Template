namespace Domain;

public sealed class TodoId : StrongTypedGuid<TodoId>
{
	public TodoId(Guid primitiveId) : base(primitiveId)
	{
	}
}

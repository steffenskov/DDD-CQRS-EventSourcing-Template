namespace Domain;

public class TodoId : StrongTypedGuid<TodoId>
{
	public TodoId(Guid primitiveId) : base(primitiveId)
	{
	}
}


namespace Domain;

public class CommandId : StrongTypedGuid<CommandId>
{
	public CommandId(Guid primitiveId) : base(primitiveId)
	{
	}
}

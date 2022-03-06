using api.Models;
using core.Todos.Commands;
using core.Todos.Queries;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController : ControllerBase
{
	private IMediator _mediator;
	public TodoController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost]
	public async Task<ActionResult<TodoViewModel>> PostAsync(TodoInputModel createModel, CancellationToken cancellationToken)
	{
		var command = new TodoCreateCommand(Guid.NewGuid(), createModel.Title, createModel.Body, createModel.DueDate);

		var result = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
		return new ObjectResult(new TodoViewModel(result))
		{
			StatusCode = 201
		};
	}

	[HttpPut("{id}")]
	public async Task<ActionResult<TodoViewModel>> PatchDueDateAsync(Guid id, TodoInputModel updateModel, CancellationToken cancellationToken)
	{
		var command = new TodoUpdateCommand(id, updateModel.Title, updateModel.Body, updateModel.DueDate);

		var result = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
		return NoContent(); // We could return the view model instead if we wanted, as result is actually our aggregate
	}

	[HttpPatch("{id}/DueDate")]
	public async Task<IActionResult> PatchDueDateAsync(Guid id, TodoDueDateInputModel updateModel, CancellationToken cancellationToken)
	{
		var command = new TodoUpdateDueDateCommand(id, updateModel.DueDate);

		var result = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
		return NoContent(); // We could return the view model instead if we wanted, as result is actually our aggregate
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
	{
		var command = new TodoDeleteCommand(id);

		var result = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
		return NoContent();
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<TodoViewModel>> GetAsync(Guid id, CancellationToken cancellationToken)
	{
		var command = new TodoGetSingleQuery(id);

		var result = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
		if (result != null)
			return new TodoViewModel(result!);
		else
			return NotFound();
	}


	[HttpGet]
	public async Task<ActionResult<ICollection<TodoViewModel>>> GetAllAsync(CancellationToken cancellationToken)
	{
		var command = new TodoGetAllQuery();

		var result = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
		if (result != null)
		{
			return result
						.Select(todo => new TodoViewModel(todo))
						.ToList();
		}
		else
			return NotFound(); // It's up for discussion whether 404 is really the proper pick for an empty collection, do what makes sense in your project.
	}
}

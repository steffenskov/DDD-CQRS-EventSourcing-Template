using System.Net;
using Api.Todos.Models;
using Domain;
using Domain.Todos.Commands;
using Domain.Todos.Queries;

namespace Api.Controllers;

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
	[ProducesResponseType(StatusCodes.Status201Created)]
	public async Task<ActionResult<TodoViewModel>> PostAsync(TodoInputModel createModel, CancellationToken cancellationToken)
	{
		var command = new TodoCreateCommand(TodoId.New(), createModel.Title, createModel.Body, createModel.DueDate);

		var result = await _mediator.Send(command, cancellationToken);

		return new ObjectResult(new TodoViewModel(result))
		{
			StatusCode = 201
		};
	}

	[HttpPut("{id}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<TodoViewModel>> UpdateAsync(TodoId id, TodoInputModel updateModel, CancellationToken cancellationToken)
	{
		var command = new TodoUpdateCommand(id, updateModel.Title, updateModel.Body, updateModel.DueDate);

		var result = await _mediator.Send(command, cancellationToken); // Will throw AggregateNotFoundException if Id does not exist, this should be converted into a 404 by Middleware
		return NoContent(); // We could return the view model instead if we wanted, as result is actually our aggregate
	}

	[HttpPatch("{id}/DueDate")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> PatchDueDateAsync(TodoId id, TodoDueDateInputModel updateModel, CancellationToken cancellationToken)
	{
		var command = new TodoUpdateDueDateCommand(id, updateModel.DueDate);

		var result = await _mediator.Send(command, cancellationToken); // Will throw AggregateNotFoundException if Id does not exist, this should be converted into a 404 by Middleware
		return NoContent(); // We could return the view model instead if we wanted, as result is actually our aggregate
	}

	[HttpDelete("{id}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> DeleteAsync(TodoId id, CancellationToken cancellationToken)
	{
		var command = new TodoDeleteCommand(id);

		var result = await _mediator.Send(command, cancellationToken); // Will throw AggregateNotFoundException if Id does not exist, this should be converted into a 404 by Middleware
		return NoContent(); // We could return the view model instead if we wanted, as result is actually our aggregate
	}

	[HttpGet("{id}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<TodoViewModel>> GetAsync(TodoId id, CancellationToken cancellationToken)
	{
		var command = new TodoGetSingleQuery(id);

		var result = await _mediator.Send(command, cancellationToken);
		return result is not null
				? new TodoViewModel(result)
				: NotFound();
	}


	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public async Task<ActionResult<ICollection<TodoViewModel>>> GetAllAsync(CancellationToken cancellationToken)
	{
		var command = new TodoGetAllQuery();

		var result = await _mediator.Send(command, cancellationToken);
		return result
					.Select(todo => new TodoViewModel(todo))
					.ToList();
	}
}

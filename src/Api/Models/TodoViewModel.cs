using Domain;
using Domain.Todos;

namespace Api.Models;

public class TodoViewModel
{
	public TodoId Id { get; }
	public string Title { get; }

	public string Body { get; }

	public DateTime DueDate { get; }

	public TodoViewModel(Todo todo) // We could also use AutoMapper instead of manually mapping
	{
		this.Id = todo.Id;
		this.Title = todo.Title;
		this.Body = todo.Body;
		this.DueDate = todo.DueDate;
	}
}

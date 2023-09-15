namespace Domain.Todos.Commands.Abstractions;

public abstract record BaseTodoCommand(TodoId AggregateId) : EventSourcedCommand<Todo, TodoId>(AggregateId);
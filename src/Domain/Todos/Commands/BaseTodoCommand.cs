namespace Domain.Todos.Commands;

public abstract record BaseTodoCommand(TodoId AggregateId) : EventSourcedCommand<Todo, TodoId>(AggregateId);
using Domain.Todos.Aggregates;
using Domain.Todos.Commands;

namespace Domain.Todos.Notifications;

internal record TodoPersistNotification(Todo Aggregate, BaseTodoCommand Command) : INotification;

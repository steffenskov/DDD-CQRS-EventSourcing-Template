using Domain.Todos.Aggregates;
using Domain.Todos.Commands;

namespace Domain.Todos.Notifications;

internal record TodoPersistNotification(Todo Aggregate, ITodoCommand Command) : INotification;

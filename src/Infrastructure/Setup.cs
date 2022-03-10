using Infrastructure.Todos.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;
public static class Setup
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services)
	{
		services.AddTransient<ITodoSnapshotRepository, TodoSnapshotRepository>();
		services.AddTransient<ITodoEventSourcedRepository, TodoEventSourcedRepository>();
		return services;
	}
}

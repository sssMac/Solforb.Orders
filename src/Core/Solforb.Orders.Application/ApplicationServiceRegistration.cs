using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace Solforb.Orders.Application
{
	public static class ApplicationServiceRegistration
	{
		public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
		{
			services.AddAutoMapper(Assembly.GetExecutingAssembly());
			services.AddMediatR(Assembly.GetExecutingAssembly());

			return services;
		}
	}
}

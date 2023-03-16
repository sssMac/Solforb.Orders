using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Solforb.Orders.Application.Persistence.Contracts;
using Solforb.Orders.Persistence.Repositories;

namespace Solforb.Orders.Persistence
{
	public static class PersistenceServicesRegistration
	{
		public static IServiceCollection AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<OrdersDbContext>(options =>
			   options.UseNpgsql(configuration.GetConnectionString("OrdersDbConnectionString")));

			services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			services.AddScoped<IUnitOfWork, UnitOfWork>();

			return services;
		}
	}
}

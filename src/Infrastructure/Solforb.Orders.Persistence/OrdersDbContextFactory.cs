using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;


namespace Solforb.Orders.Persistence
{
	public class OrdersDbContextFactory : IDesignTimeDbContextFactory<OrdersDbContext>
	{
		public OrdersDbContext CreateDbContext(string[] args)
		{
			IConfigurationRoot configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.Build();

			var builder = new DbContextOptionsBuilder<OrdersDbContext>();
			var connectionString = configuration.GetConnectionString("OrdersDbConnectionString");

			builder.UseNpgsql(connectionString);

			return new OrdersDbContext(builder.Options);
		}
	}
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Solforb.Orders.Domain;

namespace Solforb.Orders.Persistence
{
	public class OrdersDbContext : DbContext
	{
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderItem> OrderItems { get; set; }
		public DbSet<Provider> Providers { get; set; }

		public OrdersDbContext(DbContextOptions<OrdersDbContext> options) 
			: base(options){ }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrdersDbContext).Assembly);
		}
	}
}

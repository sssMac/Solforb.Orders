using Solforb.Orders.Application.Persistence.Contracts;
using Solforb.Orders.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solforb.Orders.Persistence.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly OrdersDbContext _context;

		private IGenericRepository<Order> _orderRepository;
		private IGenericRepository<OrderItem> _orderItemRepository;
		private IGenericRepository<Provider> _providerRepository;
		public UnitOfWork(OrdersDbContext context)
		{
			_context = context;
		}

		public IGenericRepository<Order> OrderRepository =>
			_orderRepository ??= new GenericRepository<Order>(_context);
		public IGenericRepository<OrderItem> OrderItemRepository =>
			_orderItemRepository ??= new GenericRepository<OrderItem>(_context);
		public IGenericRepository<Provider> ProviderRepository =>
			_providerRepository ??= new GenericRepository<Provider>(_context);

		public void Dispose()
		{
			_context.Dispose();
			GC.SuppressFinalize(this);
		}

		public async Task Save()
		{
			await _context.SaveChangesAsync();
		}

	}
}

using Solforb.Orders.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solforb.Orders.Application.Persistence.Contracts
{
	public interface IUnitOfWork
	{
		public IGenericRepository<Order> OrderRepository { get; }
		public IGenericRepository<OrderItem> OrderItemRepository { get; }
		public IGenericRepository<Provider> ProviderRepository { get; }


		public Task Save();
		public void Dispose();
	}
}

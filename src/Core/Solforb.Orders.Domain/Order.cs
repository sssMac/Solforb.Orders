using Solforb.Orders.Domain.Common;

namespace Solforb.Orders.Domain
{
	public class Order : BaseDomainEntity
	{
		public string Number { get; set; }
		public DateTime Date { get; set; }
		public int ProviderId { get; set; }
		public List<OrderItem> OrderItems { get; set;}

		public Provider Provider { get; set; }
	}
}

using Solforb.Orders.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solforb.Orders.Domain
{
	public class Order : BaseDomainEntity
	{
		public string Number { get; set; }
		public DateTime Date { get; set; }
		public int ProviderId { get; set; }

		public List<OrderItem> OrderItems { get; set;}
	}
}

using Solforb.Orders.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solforb.Orders.Domain
{
	public class OrderItem : BaseDomainEntity
	{
		public int OrderId { get; set; }
		public string Name { get; set; }

		public decimal Quantity { get; set; }
		public string Unit { get; set; }
	}
}

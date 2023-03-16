using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solforb.Orders.Application.DTOs.Common
{
    public interface IOrderItemDto
    {
		public string Name { get; set; }
		public decimal Quantity { get; set; }
		public string Unit { get; set; }
	}
}

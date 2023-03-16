using Solforb.Orders.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solforb.Orders.Application.DTOs.Order
{
	public class CreateOrderDto : IOrderDto
	{
		public string Number { get; set; }
		public DateTime Date { get; set; }
		public int ProviderId { get; set; }
		public List<CreateOrderItemDto> OrderItemsDto { get; set; }
	}
}

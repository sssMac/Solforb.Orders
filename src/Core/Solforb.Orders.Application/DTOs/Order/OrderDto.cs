using Solforb.Orders.Application.DTOs.Common;
using Solforb.Orders.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solforb.Orders.Application.DTOs.Order
{
    public class OrderDto : BaseDto
    {
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public int ProviderId { get; set; }

		public List<OrderItemDto> OrderItems { get; set; }
	}
}

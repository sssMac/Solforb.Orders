using Solforb.Orders.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solforb.Orders.Application.DTOs.Order
{
    public class OrderItemDto : BaseDto
    {
        public int OrderId { get; set; }
        public string Name { get; set; }

        public decimal Quantity { get; set; }
        public string Unit { get; set; }
    }
}

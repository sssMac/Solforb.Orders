using MediatR;
using Solforb.Orders.Application.DTOs.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solforb.Orders.Application.Features.Orders.Requests.Commands
{
	public class UpdateOrderCommand : IRequest<Unit>
	{
		public UpdateOrderDto OrderDto { get; set; }
	}
}

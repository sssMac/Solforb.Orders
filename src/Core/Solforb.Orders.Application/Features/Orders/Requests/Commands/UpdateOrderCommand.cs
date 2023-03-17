using MediatR;
using Solforb.Orders.Application.DTOs.Order;
using Solforb.Orders.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solforb.Orders.Application.Features.Orders.Requests.Commands
{
	public class UpdateOrderCommand : IRequest<BaseCommandResponse>
	{
		public int Id { get; set; }
		public UpdateOrderDto OrderDto { get; set; }
	}
}

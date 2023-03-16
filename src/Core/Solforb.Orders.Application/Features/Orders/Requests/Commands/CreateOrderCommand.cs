using MediatR;
using Solforb.Orders.Application.DTOs.Order;
using Solforb.Orders.Application.Responses;

namespace Solforb.Orders.Application.Features.Orders.Requests.Commands
{
    public class CreateOrderCommand : IRequest<BaseCommandResponse>
	{
		public CreateOrderDto OrderDto { get; set; }
	}
}

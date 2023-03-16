using MediatR;
using Solforb.Orders.Application.DTOs.Order;


namespace Solforb.Orders.Application.Features.Orders.Requests.Commands
{
    public class CreateOrderCommand : IRequest<int>
	{
		public CreateOrderDto OrderDto { get; set; }
	}
}

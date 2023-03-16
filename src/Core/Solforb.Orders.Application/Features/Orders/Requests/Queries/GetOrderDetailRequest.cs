using MediatR;
using Solforb.Orders.Application.DTOs.Order;

namespace Solforb.Orders.Application.Features.Orders.Requests.Queries
{
    public class GetOrderDetailRequest : IRequest<OrderDto>
	{
        public int Id { get; set; }
    }
}

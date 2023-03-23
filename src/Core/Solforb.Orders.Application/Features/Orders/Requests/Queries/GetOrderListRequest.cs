using MediatR;
using Solforb.Orders.Application.DTOs.Order;

namespace Solforb.Orders.Application.Features.Orders.Requests.Queries
{
    public class GetOrderListRequest :IRequest<List<OrderDto>>
    {
		public string? Number { get; set; }
		public DateTime? DateMin { get; set; }
		public DateTime? DateMax { get; set; }
		public string? ItemName { get; set; }
		public string? Unit { get; set; }
		public string? ProviderName { get; set; }

	}
}

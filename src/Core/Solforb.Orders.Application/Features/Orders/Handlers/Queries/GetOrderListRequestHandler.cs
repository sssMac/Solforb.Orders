using AutoMapper;
using MediatR;
using Solforb.Orders.Application.DTOs.Order;
using Solforb.Orders.Application.Features.Common;
using Solforb.Orders.Application.Features.Orders.Requests.Queries;
using Solforb.Orders.Application.Persistence.Contracts;

namespace Solforb.Orders.Application.Features.Orders.Handlers.Queries
{
    public class GetOrderListRequestHandler : BaseRequestHandler<GetOrderListRequest, List<OrderDto>>
	{
		public GetOrderListRequestHandler(IUnitOfWork unitOfWork, IMapper mapper) 
			: base(unitOfWork, mapper)
		{
		}

		public override async Task<List<OrderDto>> Handle(GetOrderListRequest request, CancellationToken cancellationToken)
		{
			var orders = await _unitOfWork.OrderRepository.Get();

			return _mapper.Map<List<OrderDto>>(orders);
		}
	}
}

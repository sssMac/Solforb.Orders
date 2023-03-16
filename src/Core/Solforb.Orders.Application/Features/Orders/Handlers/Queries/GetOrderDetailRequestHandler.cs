using AutoMapper;
using MediatR;
using Solforb.Orders.Application.DTOs.Order;
using Solforb.Orders.Application.Features.Common;
using Solforb.Orders.Application.Features.Orders.Requests.Queries;
using Solforb.Orders.Application.Persistence.Contracts;

namespace Solforb.Orders.Application.Features.Orders.Handlers.Queries
{
    public class GetOrderDetailRequestHandler : BaseRequestHandler<GetOrderDetailRequest, OrderDto>
	{
		public GetOrderDetailRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
			: base(unitOfWork, mapper)
		{
		}

		public override async Task<OrderDto> Handle(GetOrderDetailRequest request, CancellationToken cancellationToken)
		{
			var order = await _unitOfWork.OrderRepository.GetByID(request.Id);

			return _mapper.Map<OrderDto>(order);
		}
	}
}

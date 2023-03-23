using AutoMapper;
using MediatR;
using Solforb.Orders.Application.DTOs.Order;
using Solforb.Orders.Application.Extensions;
using Solforb.Orders.Application.Features.Common;
using Solforb.Orders.Application.Features.Orders.Requests.Queries;
using Solforb.Orders.Application.Persistence.Contracts;
using Solforb.Orders.Domain;
using System.Linq.Expressions;
using System.Xml.Linq;

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
			var where = ExpressionExtension.True<Order>();

			if (request != null)
			{

				if (!string.IsNullOrEmpty(request.Number))
					where = where.And(o => o.Number.ToLower().Contains(request.Number.ToLower()));

				if (request.DateMin.HasValue)
					where = where.And(o => o.Date >= TimeZoneInfo.ConvertTimeToUtc(request.DateMin.Value.AddHours(24)));

				if (request.DateMax.HasValue)
					where = where.And(o => o.Date <= TimeZoneInfo.ConvertTimeToUtc(request.DateMax.Value.AddHours(24)));

				if (!string.IsNullOrEmpty(request.ItemName))
					where = where.And(o => o.OrderItems.Any(i => i.Name.ToLower().Contains(request.ItemName.ToLower())));

				if (!string.IsNullOrEmpty(request.Unit))
					where = where.And(o => o.OrderItems.Any(i => i.Unit.Contains(request.Unit)));

				if (!string.IsNullOrEmpty(request.ProviderName))
					where = where.And(o => o.Provider.Name.ToLower().Contains(request.ProviderName.ToLower()));
			}

			var orders = await _unitOfWork.OrderRepository.Get(where, default, "OrderItems");

			return _mapper.Map<List<OrderDto>>(orders);
		}


	}
}

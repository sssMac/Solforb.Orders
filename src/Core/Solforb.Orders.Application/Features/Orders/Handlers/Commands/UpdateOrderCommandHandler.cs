using AutoMapper;
using MediatR;
using Solforb.Orders.Application.Features.Common;
using Solforb.Orders.Application.Features.Orders.Requests.Commands;
using Solforb.Orders.Application.Persistence.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solforb.Orders.Application.Features.Orders.Handlers.Commands
{
	public class UpdateOrderCommandHandler : BaseRequestHandler<UpdateOrderCommand, Unit>
	{
		public UpdateOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) 
			: base(unitOfWork, mapper)
		{
		}

		public override async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
		{
			var order = await _unitOfWork.OrderRepository.GetByID(request.OrderDto.Id);

			_mapper.Map(request.OrderDto, order);
			_mapper.Map(request.OrderDto.OrderItemsDto, order.OrderItems);

			await _unitOfWork.OrderRepository.Update(order);
			await _unitOfWork.OrderItemRepository.Update(order.OrderItems);
			await _unitOfWork.SaveChagesAsync();

			return Unit.Value;
		}
	}
}

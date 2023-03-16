using AutoMapper;
using Solforb.Orders.Application.Features.Common;
using Solforb.Orders.Application.Features.Orders.Requests.Commands;
using Solforb.Orders.Application.Persistence.Contracts;
using Solforb.Orders.Domain;

namespace Solforb.Orders.Application.Features.Orders.Handlers.Commands
{
	public class CreateOrderCommandHandler : BaseRequestHandler<CreateOrderCommand, int>
	{
		public CreateOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) 
			: base(unitOfWork, mapper)
		{
		}

		public override async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
		{
			var order = _mapper.Map<Order>(request.OrderDto);
			var orderItems = _mapper.Map<List<OrderItem>>(request.OrderDto.OrderItemsDto);

			order = await _unitOfWork.OrderRepository.Insert(order);

			orderItems.ForEach(i => i.OrderId = order.Id);

			await _unitOfWork.OrderItemRepository.Insert(orderItems);
			await _unitOfWork.SaveChagesAsync();

			return order.Id;


		}
	}
}

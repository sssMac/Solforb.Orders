using AutoMapper;
using MediatR;
using Solforb.Orders.Application.DTOs.Order.Validators;
using Solforb.Orders.Application.Exceptions;
using Solforb.Orders.Application.Features.Common;
using Solforb.Orders.Application.Features.Orders.Requests.Commands;
using Solforb.Orders.Application.Persistence.Contracts;

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
			// ORDER VALIDATION
			var validator = new UpdateOrderDtoValidator(_unitOfWork);
			var validatorResult = await validator.ValidateAsync(request.OrderDto);

			if (!validatorResult.IsValid)
				throw new ValidationException(validatorResult);

			// ORDER ITEM VALIDATION
			var validatorItems = new UpdateOrderItemDtoValidator(_unitOfWork);
			request.OrderDto.OrderItemsDto.ForEach(async p =>
			{
				var validationItemResult = await validatorItems.ValidateAsync(p);
				if (validationItemResult.IsValid)
					throw new ValidationException(validationItemResult);
			});

			// LOGIC
			var order = await _unitOfWork.OrderRepository.GetByID(request.Id);

			_mapper.Map(request.OrderDto, order);
			_mapper.Map(request.OrderDto.OrderItemsDto, order.OrderItems);

			await _unitOfWork.OrderRepository.Update(order);
			await _unitOfWork.OrderItemRepository.UpdateRange(order.OrderItems);

			return Unit.Value;
		}
	}
}

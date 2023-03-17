using AutoMapper;
using MediatR;
using Solforb.Orders.Application.DTOs.Order.Validators;
using Solforb.Orders.Application.Exceptions;
using Solforb.Orders.Application.Features.Common;
using Solforb.Orders.Application.Features.Orders.Requests.Commands;
using Solforb.Orders.Application.Persistence.Contracts;
using Solforb.Orders.Application.Responses;

namespace Solforb.Orders.Application.Features.Orders.Handlers.Commands
{
	public class UpdateOrderCommandHandler : BaseRequestHandler<UpdateOrderCommand, BaseCommandResponse>
	{
		public UpdateOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) 
			: base(unitOfWork, mapper)
		{
		}

		public override async Task<BaseCommandResponse> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
		{
			var response = new BaseCommandResponse();

			// ORDER VALIDATION
			var validator = new UpdateOrderDtoValidator(_unitOfWork);
			var validatorResult = await validator.ValidateAsync(request.OrderDto);

			if (!validatorResult.IsValid)
			{
				response.Success = false;
				response.Message = "Update failed";
				response.Errors = validatorResult.Errors.Select(q => q.ErrorMessage).ToList();
			}

			// ORDER ITEM VALIDATION
			var validatorItems = new UpdateOrderItemDtoValidator(_unitOfWork);
			request.OrderDto.OrderItems.ForEach(async p =>
			{
				var validationItemResult = await validatorItems.ValidateAsync(p);
				if (!validationItemResult.IsValid)
				{
					response.Success = false;
					response.Message = "Update failed";
					response.Errors = validationItemResult.Errors.Select(q => q.ErrorMessage).ToList();
				}
			});

			if (response.Success)
			{
				// LOGIC
				var order = await _unitOfWork.OrderRepository.GetByID(request.Id, "OrderItems");

				_mapper.Map(request.OrderDto, order);

				await _unitOfWork.OrderRepository.Update(order);
				await _unitOfWork.Save();

				response.Success = true;
				response.Message = "Update Successful";
				response.Id = order.Id;
			}

			return response;
		}
	}
}

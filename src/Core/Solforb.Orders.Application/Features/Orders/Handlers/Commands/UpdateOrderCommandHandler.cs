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
			BaseCommandResponse result = BaseCommandResponse.Succeed();


			// ORDER VALIDATION
			var validator = new UpdateOrderDtoValidator(_unitOfWork);
			var validatorResult = await validator.ValidateAsync(request.OrderDto);

			// IF INVALID
			if (!validatorResult.IsValid)
			{
				result = BaseCommandResponse.Failed(validatorResult.Errors.Select(q => q.ErrorMessage).ToList());
			}

			// IF VALID
			if (result.Success)
			{
				var order = await _unitOfWork.OrderRepository.GetByID(request.Id, "OrderItems");

				_mapper.Map(request.OrderDto, order);

				await _unitOfWork.OrderRepository.Update(order);
				await _unitOfWork.Save();

			}

			return result;
		}
	}
}

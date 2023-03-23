using AutoMapper;
using Solforb.Orders.Application.DTOs.Order.Validators;
using Solforb.Orders.Application.Exceptions;
using Solforb.Orders.Application.Features.Common;
using Solforb.Orders.Application.Features.Orders.Requests.Commands;
using Solforb.Orders.Application.Persistence.Contracts;
using Solforb.Orders.Application.Responses;
using Solforb.Orders.Domain;

namespace Solforb.Orders.Application.Features.Orders.Handlers.Commands
{
	public class CreateOrderCommandHandler : BaseRequestHandler<CreateOrderCommand, BaseCommandResponse>
	{
		public CreateOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) 
			: base(unitOfWork, mapper)
		{
		}

		public override async Task<BaseCommandResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
		{
			BaseCommandResponse result = BaseCommandResponse.Succeed();

			// ORDER VALIDATION
			var validator = new CreateOrderDtoValidator(_unitOfWork);
			var validatorResult = await validator.ValidateAsync(request.OrderDto);

			// IF INVALID
			if (!validatorResult.IsValid)
			{
				result = BaseCommandResponse.Failed(validatorResult.Errors.Select(q => q.ErrorMessage).ToList());
			}

			// IF VALID
			if (result.Success)
			{
				var order = _mapper.Map<Order>(request.OrderDto);
				order = await _unitOfWork.OrderRepository.Insert(order);
				await _unitOfWork.Save();

			}
			return result;

		}
	}
}

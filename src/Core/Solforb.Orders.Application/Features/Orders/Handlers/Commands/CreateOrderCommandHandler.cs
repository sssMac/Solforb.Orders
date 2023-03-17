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
			var response = new BaseCommandResponse();
			// ORDER VALIDATION
			var validator = new CreateOrderDtoValidator(_unitOfWork);
			var validatorResult = await validator.ValidateAsync(request.OrderDto);

			if (!validatorResult.IsValid)
			{
				response.Success = false;
				response.Message = "Creation failed";
				response.Errors = validatorResult.Errors.Select(q => q.ErrorMessage).ToList();
			}

			// ORDER ITEM VALIDATION
			var validatorItems = new CreateOrderItemDtoValidator(_unitOfWork);
			request.OrderDto.OrderItems.ForEach(async p =>
			{
				var validationItemResult = await validatorItems.ValidateAsync(p);
				if (!validationItemResult.IsValid)
				{
					response.Success = false;
					response.Message = "Creation failed";
					response.Errors = validationItemResult.Errors.Select(q => q.ErrorMessage).ToList();
				}
			});

			// LOGIC
			if (response.Success)
			{
				var order = _mapper.Map<Order>(request.OrderDto);

				order = await _unitOfWork.OrderRepository.Insert(order);
				await _unitOfWork.Save();

				response.Success = true;
				response.Message = "Creation Successful";
				response.Id = order.Id;

			}
			return response;

		}
	}
}

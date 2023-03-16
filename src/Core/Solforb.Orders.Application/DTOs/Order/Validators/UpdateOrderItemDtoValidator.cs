using FluentValidation;
using Solforb.Orders.Application.Persistence.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solforb.Orders.Application.DTOs.Order.Validators
{
	public class UpdateOrderItemDtoValidator : AbstractValidator<OrderItemDto>
	{
		public UpdateOrderItemDtoValidator(IUnitOfWork unitOfWork)
		{
			Include(new IOrderItemValidator(unitOfWork));
			RuleFor(p => p.Id)
				.NotNull().WithMessage("{PropertyName} must be present.");

			RuleFor(p => p.OrderId)
				.GreaterThan(0)
				.MustAsync(async (id, token) =>
				{
					var orderExists = await unitOfWork.OrderRepository.Exists(id);
					return !orderExists;

				}).WithMessage("{PropertyName} does not exist.");
		}
	}
}

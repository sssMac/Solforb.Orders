using FluentValidation;
using Solforb.Orders.Application.Persistence.Contracts;
using Solforb.Orders.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solforb.Orders.Application.DTOs.Order.Validators
{
	public class CreateOrderDtoValidator : AbstractValidator<CreateOrderDto>
	{
		public CreateOrderDtoValidator(IUnitOfWork unitOfWork)
		{
			Include(new IOrderDtoValidator(unitOfWork));

			RuleFor(order => order)
				.Must((order, token) =>
				{
					var item = order.OrderItems.FirstOrDefault(i => i.Name == order.Number);

					return item == null ? true : false;
				}).WithMessage("The subject of an order cannot be named as an order.")
				.MustAsync(async (order, token) =>
				{
					var orderExist = (await unitOfWork.OrderRepository.Get(p => p.Number == order.Number && p.ProviderId == order.ProviderId)).FirstOrDefault();
					return orderExist == null ? true : false;
				}).WithMessage("The provider already has an order with this number.");

			RuleForEach(order => order.OrderItems).SetValidator(new CreateOrderItemDtoValidator(unitOfWork));
		}
	}
}

using FluentValidation;
using Solforb.Orders.Application.Persistence.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solforb.Orders.Application.DTOs.Order.Validators
{
	public class UpdateOrderItemDtoValidator : AbstractValidator<UpdateOrderItemDto>
	{
		public UpdateOrderItemDtoValidator(IUnitOfWork unitOfWork)
		{
			Include(new IOrderItemValidator(unitOfWork));

		}
	}
}

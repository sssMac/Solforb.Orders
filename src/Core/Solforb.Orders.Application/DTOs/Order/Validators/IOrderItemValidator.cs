using FluentValidation;
using Solforb.Orders.Application.DTOs.Common;
using Solforb.Orders.Application.Persistence.Contracts;

namespace Solforb.Orders.Application.DTOs.Order.Validators
{
	public class IOrderItemValidator : AbstractValidator<IOrderItemDto>
	{
		private readonly IUnitOfWork _unitOfWork;
		public IOrderItemValidator(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;

			RuleFor(p => p.Name)
				.NotEmpty().WithMessage("{PropertyName} is required.")
				.NotNull()
				.MaximumLength(50).WithMessage("{PropertyName} must not exceed {ComaprisonValue} characters.");

			RuleFor(p => p.Quantity)
				.NotEmpty().WithMessage("{PropertyName} is required.")
				.NotNull()
				.GreaterThan(0.1M).WithMessage("{PropertyName} must be at least {ComaprisonValue}.")
				.LessThan(10000).WithMessage("{PropertyName} must be less than {ComaprisonValue}.");

			RuleFor(p => p.Unit)
				.NotEmpty().WithMessage("{PropertyName} is required.")
				.NotNull()
				.MaximumLength(20).WithMessage("{PropertyName} must not exceed {ComaprisonValue} characters.");
		}
	}
}

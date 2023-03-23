using FluentValidation;
using Solforb.Orders.Application.DTOs.Common;
using Solforb.Orders.Application.Persistence.Contracts;

namespace Solforb.Orders.Application.DTOs.Order.Validators
{
	public class IOrderDtoValidator : AbstractValidator<IOrderDto>
	{
		private readonly IUnitOfWork _unitOfWork;
		public IOrderDtoValidator(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
				
			RuleFor(p => p.Number)
				.NotEmpty().WithMessage("{PropertyName} is required.")
				.NotNull()
				.MaximumLength(50).WithMessage("{PropertyName} must not exceed {ComaprisonValue} characters.");

			RuleFor(p => p.Date)
				.NotEmpty().WithMessage("{PropertyName} is required.");

			RuleFor(p => p.ProviderId)
				.GreaterThan(0)
				.MustAsync(async (id, token) =>
				{
					var providerExists = await _unitOfWork.ProviderRepository.Exists(id);
					return providerExists;
				}).WithMessage("{PropertyName} does not exist.");

		}
	}
}

using AutoMapper;
using MediatR;
using Solforb.Orders.Application.Exceptions;
using Solforb.Orders.Application.Features.Common;
using Solforb.Orders.Application.Features.Orders.Requests.Commands;
using Solforb.Orders.Application.Persistence.Contracts;
using Solforb.Orders.Domain;

namespace Solforb.Orders.Application.Features.Orders.Handlers.Commands
{
	public class DeleteOrderCommandHandler : BaseRequestHandler<DeleteOrderCommand, Unit>
	{
		public DeleteOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) 
			: base(unitOfWork, mapper)
		{
		}

		public override async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
		{
			var order = await _unitOfWork.OrderRepository.GetByID(request.Id);

			if (order == null)
				throw new NotFoundException(nameof(Order), request.Id);

			await _unitOfWork.OrderRepository.Delete(order);

			return Unit.Value;
		}
	}
}

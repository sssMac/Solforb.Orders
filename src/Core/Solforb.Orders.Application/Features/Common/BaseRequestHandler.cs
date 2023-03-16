using AutoMapper;
using MediatR;
using Solforb.Orders.Application.DTOs.Common;
using Solforb.Orders.Application.Persistence.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solforb.Orders.Application.Features.Common
{
	public abstract class BaseRequestHandler<TReq, TResp> : IRequestHandler<TReq, TResp>
		where TReq : IRequest<TResp>
	{
		private protected readonly IUnitOfWork _unitOfWork;
		private protected readonly IMapper _mapper;

		public BaseRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public abstract Task<TResp> Handle(TReq request, CancellationToken cancellationToken);
	}
}

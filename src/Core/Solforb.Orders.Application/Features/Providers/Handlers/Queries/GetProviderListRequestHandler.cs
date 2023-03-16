using AutoMapper;
using MediatR;
using Solforb.Orders.Application.DTOs.Provider;
using Solforb.Orders.Application.Features.Common;
using Solforb.Orders.Application.Features.Orders.Requests.Queries;
using Solforb.Orders.Application.Features.Providers.Requests.Queries;
using Solforb.Orders.Application.Persistence.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solforb.Orders.Application.Features.Providers.Handlers.Queries
{
    public class GetProviderListRequestHandler : BaseRequestHandler<GetProviderListRequest, List<ProviderDto>>
	{
		public GetProviderListRequestHandler(IUnitOfWork unitOfWork, IMapper mapper) 
			: base(unitOfWork, mapper)
		{
		}

		public override async Task<List<ProviderDto>> Handle(GetProviderListRequest request, CancellationToken cancellationToken)
		{
			var providers = await _unitOfWork.ProviderRepository.Get();

			return _mapper.Map<List<ProviderDto>>(providers);
		}
	}
}

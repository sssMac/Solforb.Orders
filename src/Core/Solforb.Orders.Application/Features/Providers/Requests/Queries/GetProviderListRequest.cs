using MediatR;
using Solforb.Orders.Application.DTOs.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solforb.Orders.Application.Features.Providers.Requests.Queries
{
    public class GetProviderListRequest : IRequest<List<ProviderDto>>
	{
	}
}

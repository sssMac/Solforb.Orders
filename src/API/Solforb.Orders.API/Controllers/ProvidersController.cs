using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Solforb.Orders.Application.DTOs.Order;
using Solforb.Orders.Application.DTOs.Provider;
using Solforb.Orders.Application.Features.Orders.Requests.Queries;
using Solforb.Orders.Application.Features.Providers.Requests.Queries;

namespace Solforb.Orders.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProvidersController : ControllerBase
	{
		private readonly IMediator _mediator;

		public ProvidersController(IMediator mediator)
		{
			_mediator = mediator;
		}

		// GET: api/Providers
		[HttpGet]
		public async Task<ActionResult<List<ProviderDto>>> Get()
		{
			var orders = await _mediator.Send(new GetProviderListRequest());
			return Ok(orders);
		}
			
		// GET: api/Providers/5
		[HttpGet("{id}")]
		public async Task<ActionResult<ProviderDto>> Get(int id)
		{
			var order = await _mediator.Send(new GetProviderDetailRequest { Id = id });
			return Ok(order);
		}

	}
}

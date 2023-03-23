using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Solforb.Orders.Application.DTOs.Order;
using Solforb.Orders.Application.Features.Orders.Requests.Commands;
using Solforb.Orders.Application.Features.Orders.Requests.Queries;

namespace Solforb.Orders.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrdersController : ControllerBase
	{
		private readonly IMediator _mediator;

		public OrdersController(IMediator mediator)
		{
			_mediator = mediator;
		}

		// GET: api/Orders
		[HttpGet]
		public async Task<ActionResult<List<OrderDto>>> Get([FromQuery] GetOrderListRequest filter = null)
		{
			
			var orders = await _mediator.Send(filter);
			return Ok(orders);
		}

		// GET: api/Orders/5
		[HttpGet("{id}")]
		public async Task<ActionResult<OrderDto>> Get(int id)
		{
			var order = await _mediator.Send(new GetOrderDetailRequest { Id = id });
			return Ok(order);
		}

		// POST: api/Orders
		[HttpPost]
		public async Task<ActionResult> Post([FromBody] CreateOrderDto order)
		{
			var command = new CreateOrderCommand { OrderDto = order };
			var response = await _mediator.Send(command);
			return Ok(response);
		}

		// PUT: api/Orders/5
		[HttpPut("{id}")]
		public async Task<ActionResult> Put(int id, [FromBody] UpdateOrderDto order)
		{
			var command = new UpdateOrderCommand {Id = id, OrderDto = order };
			var response =  await _mediator.Send(command);
			return Ok(response);
		}

		// DELETE: api/Orders/5
		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id)
		{
			var command = new DeleteOrderCommand { Id = id };
			await _mediator.Send(command);
			return NoContent();
		}
	}
}

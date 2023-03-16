using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solforb.Orders.Application.Features.Orders.Requests.Commands
{
	public class DeleteOrderCommand : IRequest
	{
		public int Id { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solforb.Orders.Application.Responses
{
	public class BaseCommandResponse
	{
		
		public bool Success { get; private set; }
		public List<string> Errors { get; private set; }

		private BaseCommandResponse( bool success = true, List<string> errors = null)
		{
			Success = success;
			Errors = errors;
		}


		public static BaseCommandResponse Succeed() => new BaseCommandResponse(true);
		public static BaseCommandResponse Failed(List<string> errors) => new BaseCommandResponse(false, errors);
	}
}

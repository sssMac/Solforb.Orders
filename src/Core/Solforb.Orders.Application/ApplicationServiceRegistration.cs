using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Solforb.Orders.Application.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Solforb.Orders.Application
{
	public static class ApplicationServiceRegistration
	{
		public static IServiceCollection AddAplicationLayer(this IServiceCollection service)
		{
			service.AddAutoMapper(Assembly.GetExecutingAssembly());
			service.AddMediatR(Assembly.GetExecutingAssembly());

			return service;
		}
	}
}

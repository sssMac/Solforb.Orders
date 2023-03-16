using AutoMapper;
using Solforb.Orders.Application.DTOs.Order;
using Solforb.Orders.Application.DTOs.Provider;
using Solforb.Orders.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solforb.Orders.Application.Profiles
{
    public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Order, OrderDto>().ReverseMap();
			CreateMap<OrderItem, OrderItemDto>().ReverseMap();
			CreateMap<Provider, ProviderDto>().ReverseMap();
		}
	}
}

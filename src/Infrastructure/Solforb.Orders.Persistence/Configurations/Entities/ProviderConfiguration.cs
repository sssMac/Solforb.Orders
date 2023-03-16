using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solforb.Orders.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solforb.Orders.Persistence.Configurations.Entities
{
	public class ProviderConfiguration : IEntityTypeConfiguration<Provider>
	{
		public void Configure(EntityTypeBuilder<Provider> builder)
		{
			builder.HasData(
				new Provider { Id = 1, Name= "Adams S." },
				new Provider { Id = 2, Name= "Bennett H." },
				new Provider { Id = 3, Name= "Carter T." },
				new Provider { Id = 4, Name= "Collins B." },
				new Provider { Id = 5, Name= "Fisher K." }
				);
		}
	}
}

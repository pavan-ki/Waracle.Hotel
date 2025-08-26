using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Waracle.Hotel.RoomManagement.Domain.Entities;

namespace Waracle.Hotel.RoomManagement.Infra.EfCore.Configurations
{
    public sealed class RoomCategoryConfiguration : IEntityTypeConfiguration<RoomCategory>
    {
        public void Configure(EntityTypeBuilder<RoomCategory> builder)
        {
            builder.ToTable("RoomCategories");
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Name).IsRequired();
            builder.HasIndex(r => r.Name).IsUnique();

            builder.Property(r => r.MinCapacity).IsRequired();
            builder.Property(r => r.MaxCapacity).IsRequired();
        }
    }
}

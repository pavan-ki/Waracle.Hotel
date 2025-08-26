using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainEntities = Waracle.Hotel.RoomManagement.Domain.Entities;

namespace Waracle.Hotel.RoomManagement.Infra.EfCore.Configurations
{
    public sealed class HotelConfiguration : IEntityTypeConfiguration<DomainEntities.Hotel>
    {
        public void Configure(EntityTypeBuilder<DomainEntities.Hotel> builder)
        {
            builder.ToTable("Hotels");
            builder.HasKey(h => h.Id);

            builder.Property(h => h.Name)
                .IsRequired();

            builder.HasMany(h => h.Rooms)
                .WithOne()
                .HasForeignKey("HotelId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

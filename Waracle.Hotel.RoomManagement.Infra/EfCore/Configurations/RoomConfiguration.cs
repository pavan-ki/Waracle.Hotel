using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Waracle.Hotel.RoomManagement.Domain.Entities;
using DomainEntities = Waracle.Hotel.RoomManagement.Domain.Entities;

namespace Waracle.Hotel.RoomManagement.Infra.EfCore.Configurations
{
    public sealed class RoomConfiguration : IEntityTypeConfiguration<DomainEntities.Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.ToTable("Rooms");
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Name)
                .IsRequired();

            builder.HasOne(r => r.RoomCategory)
                .WithMany()
                .HasForeignKey("RoomCategoryId")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(r => r.Bookings)
                .WithOne()
                .HasForeignKey("RoomId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(r => r.Bookings)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}

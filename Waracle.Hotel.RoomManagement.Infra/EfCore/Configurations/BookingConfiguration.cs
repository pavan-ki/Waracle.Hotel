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
    public sealed class BookingConfiguration : IEntityTypeConfiguration<DomainEntities.Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.ToTable("Bookings");
            builder.HasKey("Id");

            builder.Property(b => b.Id).ValueGeneratedNever();

            builder.Property(b => b.ReferenceNumber)
                .IsRequired();

            builder.HasIndex(b => b.ReferenceNumber)
                .IsUnique();

            builder.OwnsOne(b => b.DateRange, dr =>
            {
                dr.Property(x => x.From).HasColumnName("From").HasColumnType("date").IsRequired();
                dr.Property(x => x.To).HasColumnName("To").HasColumnType("date").IsRequired();
            });

            builder.OwnsMany(b => b.Guests, g =>
            {
                g.ToTable("BookingGuests");
                g.WithOwner().HasForeignKey("BookingId");
                g.HasKey("BookingId", "Id");

                g.Property<int>("Id").ValueGeneratedOnAdd();

                g.Property(b => b.FirstName).IsRequired();
                g.Property(b => b.LastName).IsRequired();
                g.Property(b => b.Email).IsRequired();
            });

            builder.Navigation(b => b.Guests)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}

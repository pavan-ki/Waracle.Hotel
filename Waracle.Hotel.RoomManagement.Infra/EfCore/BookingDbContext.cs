using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainEntities = Waracle.Hotel.RoomManagement.Domain.Entities;

namespace Waracle.Hotel.RoomManagement.Infra.EfCore
{
    public sealed class BookingDbContext : DbContext
    {
        public BookingDbContext(DbContextOptions<BookingDbContext> options) 
            : base(options) 
        {
            
        }

        public DbSet<DomainEntities.Hotel> Hotels => Set<DomainEntities.Hotel>();
        public DbSet<DomainEntities.Room> Rooms => Set<DomainEntities.Room>();
        public DbSet<DomainEntities.Booking> Bookings => Set<DomainEntities.Booking>();
        public DbSet<DomainEntities.RoomCategory> RoomCategories => Set<DomainEntities.RoomCategory>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookingDbContext).Assembly);
        }
    }
}

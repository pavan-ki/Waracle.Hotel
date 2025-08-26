using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Waracle.Hotel.RoomManagement.Domain.Interfaces;
using Waracle.Hotel.RoomManagement.Infra.EfCore;
using Waracle.Hotel.RoomManagement.Infra.Repositories;
using Waracle.Hotel.RoomManagement.Infra.Services;
using Waracle.Hotel.RoomManagement.Application.Abstractions;

namespace Waracle.Hotel.RoomManagement.Infra
{
    public static class BookingInfra
    {
        public static IServiceCollection AddBookingInfra(this IServiceCollection services, IConfiguration configuration, string connectionStringName = "BookingDb")
        {
            var connectionString = configuration.GetConnectionString(connectionStringName);

            services.AddDbContext<BookingDbContext>(options =>
            {
                options.UseSqlServer(connectionString, x =>
                {
                    x.MigrationsAssembly(typeof(BookingDbContext).Assembly.FullName);
                    x.EnableRetryOnFailure(5, TimeSpan.FromSeconds(5), null);
                });
            });

            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddSingleton<IReferenceNumberGenerator, ReferenceNumberGenerator>();

            return services;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Waracle.Hotel.RoomManagement.Api.Endpoints;
using Waracle.Hotel.RoomManagement.Api.ResetServices;
using Waracle.Hotel.RoomManagement.Application.Services;
using Waracle.Hotel.RoomManagement.Domain.Entities;
using Waracle.Hotel.RoomManagement.Domain.ValueObjects;
using Waracle.Hotel.RoomManagement.Infra;
using Waracle.Hotel.RoomManagement.Infra.EfCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddBookingInfra(builder.Configuration);
builder.Services.AddScoped<BookingService>();
builder.Services.AddScoped<ISeed, Seed>();
builder.Services.AddScoped<Reset>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapBookingEndpoints();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<BookingDbContext>();
    await db.Database.MigrateAsync();

    var seeder = scope.ServiceProvider.GetRequiredService<ISeed>();
    await seeder.SeedAsync(db, CancellationToken.None);
}

app.Run();

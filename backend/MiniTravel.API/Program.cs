using Microsoft.EntityFrameworkCore;
using MiniTravel.Infrastructure.Persistence;
using MiniTravel.Application.Interfaces.Repositories;
using MiniTravel.Infrastructure.Repositories;
using MiniTravel.Application.Features.Reservas.Commands.CriarReserva;
using MiniTravel.Application.Features.Destinos.Queries.ListarDestinos;
using MiniTravel.Application.Interfaces.Queries;
using MiniTravel.Infrastructure.Queries;
using MiniTravel.Application.Interfaces.Cache;
using MiniTravel.Infrastructure.Cache;
using StackExchange.Redis;
using MiniTravel.Application.Interfaces.Messaging;
using MiniTravel.Infrastructure.Messaging;
using MiniTravel.Application.Features.Reservas.Commands.ConfirmarReserva;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IDestinoRepository, DestinoRepository>();
builder.Services.AddScoped<IReservaRepository, ReservaRepository>();
builder.Services.AddScoped<CriarReservaCommandHandler>();
builder.Services.AddScoped<IDestinoQueryService, DestinoQueryService>();
builder.Services.AddScoped<ListarDestinosQueryHandler>();
builder.Services.AddSingleton<IConnectionMultiplexer>(
    ConnectionMultiplexer.Connect(
        builder.Configuration["Redis:ConnectionString"]!));

builder.Services.AddScoped<ICacheService, RedisCacheService>();

builder.Services.AddDbContext<MiniTravelDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IMessageBus, RabbitMqMessageBus>();
builder.Services.AddScoped<ConfirmarReservaCommandHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

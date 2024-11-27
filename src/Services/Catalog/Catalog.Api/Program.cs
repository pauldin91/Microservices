using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handler;
using Catalog.Api.Data;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
})
    .AddValidatorsFromAssembly(assembly)
    .AddCarter()
    .AddMarten(cfg =>
    {
        cfg.Connection(builder.Configuration.GetConnectionString(nameof(ApplicationDbContext))!);
    })
    .UseLightweightSessions();

if (builder.Environment.IsDevelopment())
    builder.Services.InitializeMartenWith<CatalogInitialData>();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString(nameof(ApplicationDbContext))!);

var app = builder.Build();

app.MapCarter();

app.UseExceptionHandler(opt => { });

app.UseHealthChecks("/health",
    new HealthCheckOptions { 
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

app.Run();
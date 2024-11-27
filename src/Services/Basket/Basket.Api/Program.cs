using Basket.Api.Models;

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
        cfg.Connection(builder.Configuration.GetConnectionString(nameof(BasketDbContext))!);
        cfg.Schema.For<ShoppingCart>().Identity(s => s.Username);

    })
    .UseLightweightSessions();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks();
var app = builder.Build();

app.MapCarter();
app.UseExceptionHandler(opt => { });

app.Run();
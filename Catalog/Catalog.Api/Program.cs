using BuildingBlocks.Behaviors;
using Catalog.Api.Products.CreateProduct;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CreateProductCommand).Assembly);
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services
    .AddMarten(cfg =>
    {
        cfg.Connection(builder.Configuration.GetConnectionString(nameof(ApplicationDbContext))!);
    })
    .UseLightweightSessions();

var app = builder.Build();

app.MapCarter();

app.Run();
using Discount.Grpc.Data;
using Discount.Grpc.Extensions;
using Discount.Grpc.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();

builder.Services.AddDbContext<DiscountDbContext>(cfg => {
    cfg.UseSqlite(builder.Configuration.GetConnectionString(nameof(DiscountDbContext))!);
});

var app = builder.Build();
await app.UseApplicationBuilderExtensions();
// Configure the HTTP request pipeline.

app.MapGrpcService<DiscountService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();

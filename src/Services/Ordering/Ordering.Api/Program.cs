using Ordering.Api;
using Ordering.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddEndpointsApiExplorer()
    .AddInfrastructureExtensions(builder.Configuration)
    .AddApiExtensions();

var app = builder.Build();


app.UseApiExtensions();
app.Run();
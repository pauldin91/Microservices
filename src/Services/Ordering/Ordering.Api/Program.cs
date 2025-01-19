using Ordering.Api;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddEndpointsApiExplorer()
    .AddInfrastructureExtensions(builder.Configuration)
    .AddApiExtensions();

var app = builder.Build();

app.UseApiExtensions();

if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}

app.Run();
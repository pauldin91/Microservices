using Ordering.Api;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplicationExtensions()
    .AddInfrastructureExtensions(builder.Configuration)
    .AddApiExtensions(builder.Configuration);

var app = builder.Build();

app.UseApiExtensions();

if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}

app.Run();
﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Data;
using Ordering.Infrastructure.Data;
using Ordering.Infrastructure.Data.Interceptors;

namespace Ordering.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureExtensions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
            services.AddDbContext<OrderingDbContext>((sp, cfg) =>
            {
                cfg.AddInterceptors(sp.GetRequiredService<ISaveChangesInterceptor>());
                cfg.UseNpgsql(configuration.GetConnectionString(nameof(OrderingDbContext)));
            });

            services.AddScoped<IApplicationDbContext, OrderingDbContext>();
            return services;
        }
    }
}
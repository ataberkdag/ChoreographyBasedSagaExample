using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Application.SeedWork;
using Order.Domain.Common;
using Order.Infrastructure.MassTransit;
using Order.Infrastructure.Persistence;
using Order.Infrastructure.Repositories;
using System;
namespace Order.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            #region MassTransit

            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(host: configuration.GetConnectionString("RabbitMQ"));
                });
            });

            services.AddScoped<IMassTransitHandler, MassTransitHandler>();

            #endregion MassTransit

            services.AddDbContext<PostgreDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("OrderDb"));
            });

            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

            return services;
        }
    }
}

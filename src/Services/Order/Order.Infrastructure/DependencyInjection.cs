using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Domain.Common;
using Order.Infrastructure.MassTransit;
using Order.Infrastructure.Persistence;
using Order.Infrastructure.Repositories;
using Shared.Base;
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
                    cfg.Host(host: configuration.GetConnectionString("RabbitMQ"), h =>
                    {
                        h.Username(configuration.GetSection("RabbitMQ")["UserName"]);
                        h.Password(configuration.GetSection("RabbitMQ")["Password"]);
                    });
                });
            });

            services.AddScoped<IMassTransitHandler, MassTransitHandler>();

            #endregion MassTransit

            services.AddDbContext<EfCoreDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("OrderDb"));
            });

            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderUnitOfWork, UnitOfWork.UnitOfWork>();

            return services;
        }
    }
}

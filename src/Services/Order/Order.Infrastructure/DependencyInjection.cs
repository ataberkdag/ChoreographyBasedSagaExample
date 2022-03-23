using Core.Infrastructure;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Domain.Common;
using Order.Infrastructure.Persistence;
using Order.Infrastructure.Repositories;
using System;
namespace Order.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, Action<IBusRegistrationConfigurator> massTransitConfig)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            #region MassTransit

            services.AddCoreInfrastructure(massTransitConfig);

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

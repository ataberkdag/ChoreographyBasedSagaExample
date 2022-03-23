using Core.Infrastructure;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stock.Domain.Common;
using Stock.Infrastructure.Persistence;
using Stock.Infrastructure.Repositories;
using System;

namespace Stock.Infrastructure
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
                options.UseNpgsql(configuration.GetConnectionString("StockDb"));
            });

            services.AddScoped<IStockRepository, StockRepository>();
            services.AddScoped<IStockUnitOfWork, UnitOfWork.StockUnitOfWork>();

            return services;
        }
    }
}

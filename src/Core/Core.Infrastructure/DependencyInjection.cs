using Core.Application.SeedWork;
using Core.Infrastructure.Services;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Core.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCoreInfrastructure(this IServiceCollection services, Action<IBusRegistrationConfigurator> massTransitConfig)
        { 
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            #region MassTransit

            services.AddMassTransit(massTransitConfig);

            #endregion MassTransit

            services.AddScoped<IMassTransitHandler, MassTransitHandler>();

            return services;
        }
    }
}

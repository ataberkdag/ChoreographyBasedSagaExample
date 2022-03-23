using Core.Infrastructure;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Payment.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, Action<IBusRegistrationConfigurator> massTransitConfig)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            #region MassTransit

            services.AddCoreInfrastructure(massTransitConfig);

            #endregion MassTransit

            return services;
        }
    }
}

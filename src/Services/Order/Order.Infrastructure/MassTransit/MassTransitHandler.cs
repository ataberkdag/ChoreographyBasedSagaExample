using MassTransit;
using Order.Application.SeedWork;
using Shared.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Infrastructure.MassTransit
{
    public class MassTransitHandler : IMassTransitHandler
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public MassTransitHandler(IPublishEndpoint publishEndpoint)
        {
            this._publishEndpoint = publishEndpoint;
        }

        public async Task Publish(IDomainEvent @event)
        {
            await this._publishEndpoint.Publish(@event);
        }
    }
}

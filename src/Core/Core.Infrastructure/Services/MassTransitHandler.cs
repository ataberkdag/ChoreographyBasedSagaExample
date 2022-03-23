using Core.Application.SeedWork;
using MassTransit;
using Shared.Base;
using System;
using System.Threading.Tasks;

namespace Core.Infrastructure.Services
{
    public class MassTransitHandler : IMassTransitHandler
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public MassTransitHandler(IPublishEndpoint publishEndpoint, ISendEndpointProvider sendEndpointProvider)
        {
            this._publishEndpoint = publishEndpoint;
            this._sendEndpointProvider = sendEndpointProvider;
        }

        public async Task Publish(IDomainEvent @event, Type type)
        {
            await this._publishEndpoint.Publish(@event, type);
        }

        public async Task Send(string queueName, IDomainEvent @event, Type type)
        {
            var sendEndpoint = await this._sendEndpointProvider.GetSendEndpoint(new System.Uri($"queue:{queueName}"));

            await sendEndpoint.Send(@event, type);
        }
    }
}
